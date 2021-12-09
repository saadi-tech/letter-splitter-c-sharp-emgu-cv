using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.UI;
using Emgu.CV.Structure;
using Emgu.Util;
using System.Drawing.Imaging;
using System.IO;
namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        int img_counter = 0;
        List<string> images_list = new List<string>();
        string output_path = "E:\\Output_images\\";

        public Form1()
        {
            InitializeComponent();
            output_path_box.Text = output_path;
        }


       
        private int[][] MatCreate(int rows, int cols)
        {
            int[][] result = new int[rows][];
            for (int i = 0; i < rows; ++i)
                result[i] = new int[cols];
            return result;
        }

        private void show_mat(int[][] matrix, int rows)
        {
            for(int i = 0; i < rows; i++)
            {
                Console.WriteLine("X: {0}, Y: {1}, W: {2}, H: {3}", matrix[i][0], matrix[i][1], matrix[i][2], matrix[i][3]);
            }
        }

        private List<int[]> size_filter(List<int[]> rects)
        {   
            List<int[]> result = new List<int[]>();
            for (int i = 0; i<rects.Count; i++)
            {
                if(rects[i][2]*rects[i][3] > 300)
                {
                    result.Add(rects[i]);
                }
            }

            return result;
        }
        private List<int[]> filter_rectangles_list(List<int[]> contours)
        {   
            List<int[]> result = new List<int[]>();
            int[][] rectangles = MatCreate(contours.Count, 4);
            int valid_counter = 0;
            for (int i = 0; i < contours.Count; i++)
            {
                bool valid = true;
                Rectangle current = new Rectangle(contours[i][0],contours[i][1], contours[i][2], contours[i][3]);


                for (int j = 0; j < contours.Count; j++)
                {


                    
                    if (j != i)
                    {

                        Rectangle check = new Rectangle(contours[j][0], contours[j][1], contours[j][2], contours[j][3]);
                        int centerx, centery;
                        centerx = current.X + (current.Width) / 2;
                        centery = current.Y + (current.Height) / 2;

                        

                        if ((centerx > check.X && centerx < check.X + check.Width && centery > check.Y && centery < check.Y + check.Height))
                        {
                            //Console.WriteLine("Breaking..");
                            valid = false;
                            break;

                        }
                    }
                }
                if (valid)
                {
                    //Console.WriteLine("Size: {0}, Counter: {1}, Total: {2}", contours.Size, valid_counter,i);
                    rectangles[valid_counter][0] = current.X;
                    rectangles[valid_counter][1] = current.Y;
                    rectangles[valid_counter][2] = current.Width;
                    rectangles[valid_counter][3] = current.Height;
                    int[] temp = { current.X, current.Y,current.Width,current.Height };
                    result.Add(temp);
                    valid_counter++;

                }

            }

            Console.WriteLine("Total valid rectangles: {0}", valid_counter);
            return result;
        }

        private List<int[]> remove_corner_rect(Emgu.CV.Util.VectorOfVectorOfPoint contours, int width, int height)
        {
            List<int[]> result = new List<int[]>();

            for (int i = 0; i < contours.Size; i++)
            {
                Rectangle current = CvInvoke.BoundingRectangle(contours[i]);
                if(current.X > 0.005*width && current.Y > 0.005 * height)
                {
                    int[] temp = { current.X,current.Y,current.Width,current.Height };
                    result.Add(temp);
                }

            }
            Console.WriteLine("Size of returned {0} vs Original {1}",result.Count,contours.Size);
            return result;
        }
        private int[][] filter_rectangles(Emgu.CV.Util.VectorOfVectorOfPoint contours)
        {
            int[][] rectangles = MatCreate(contours.Size, 4);
            int valid_counter = 0;
            for(int i = 0; i < contours.Size; i++)
            {
                bool valid =  true;
                Rectangle current = CvInvoke.BoundingRectangle(contours[i]);

                for (int j = 0; j < contours.Size; j++)
                {
                    
                    if (j != i)
                    {
                        
                        Rectangle check = CvInvoke.BoundingRectangle(contours[j]);
                        int centerx,centery;
                        centerx = current.X + (current.Width) / 2;
                        centery = current.Y + (current.Height) / 2;

                        if( (centerx >check.X && centerx<check.X + check.Width  && centery > check.Y && centery < check.Y + check.Height))
                        {
                            //Console.WriteLine("Breaking..");
                            valid = false;
                            break;

                        }
                    }
                }
                if (valid)
                    {
                        //Console.WriteLine("Size: {0}, Counter: {1}, Total: {2}", contours.Size, valid_counter,i);
                        rectangles[valid_counter][0] = current.X;
                        rectangles[valid_counter][1] = current.Y;
                        rectangles[valid_counter][2] = current.Width;
                        rectangles[valid_counter][3] = current.Height;
                        valid_counter++;

                    }
                
            }

            Console.WriteLine("Total valid rectangles: {0}", valid_counter);
            return rectangles;
        }
        private Image<Bgr, byte> resize_image(Image<Bgr, byte> img,int width)
        {   


            //Console.WriteLine("Hello fuckers..");
            int h = img.Height;
            int w = img.Width;

            float r = width / (float)w;

            //Console.WriteLine("r = {0}", r);
            int new_width,new_height;

            new_height = (int)(h * r);
            new_width = width;


            CvInvoke.Resize(img, img, new Size(new_width, new_height), 0, 0, Inter.Linear);    //This resizes the image to the size of Imagebox1 
            return img;
        }

        private List<int[]> sort_everything(List<int[]> rects)
        {   

            Console.WriteLine("Total rectangles: {0}",rects.Count);
            List<int[]> final_list = new List<int[]>();

            List<int[]> line = new List<int[]>();
            int current_line = 1;

            int line_mid = (int)(rects[0][1]+(int)rects[0][3] / 2);
            int line_mid_upper = line_mid - (int)(rects[0][3] / 2);
            int line_mid_lower = line_mid + (int)(rects[0][3] / 2);
            Console.WriteLine("Line-Mid: {0}", line_mid);
            int center = 0;
            
           
            for (int i = 1; i < rects.Count; i++)
            {
                Console.WriteLine("Line-Mid: {0}, Rect_Y: {1}, Rect_Height: {2}", line_mid, rects[i][1], rects[i][3]);
                //if (rects[i][1] <= line_mid && rects[i][1] + rects[i][3] >= line_mid)
                if ( (rects[i][1]+(int)rects[i][3]/2)>line_mid_lower && (rects[i][1] + (int)rects[i][3] / 2) <= line_mid_upper)
                {
                   
                    
                    Console.WriteLine("SL");
                    line.Add(rects[i]);
                }
                else
                {
                    Console.WriteLine("NL");
                    //line ended...
                    //Console.WriteLine("Line ended...");
                    var sorted = (from item in line
                                  orderby (item[0]) descending
                                  select item).ToList();
                    for(int j = 0; j < sorted.Count; j++)
                    {
                        final_list.Add(sorted[j]);
                    }
                    current_line++;
                    line.Clear();
                    line.Add(rects[i]);
                    line_mid = (rects[i][1] + (int)rects[i][3] / 2);
                    line_mid_lower = line_mid - (int)(rects[i][3] / 2);
                    line_mid_upper = line_mid + (int)(rects[i][3] / 2);

                }
            }


            Console.WriteLine("All sorted....");
            Console.WriteLine("Total lines found: {0}",current_line);
            return final_list;
        }
        private void find_words(string filename)
        {   
            
            GC.Collect();
            bool resized = false;
            Image<Bgr, byte> img = new Image<Bgr, byte>(filename);

            if (img.Width < 1200)
            {
                
                img = resize_image(img, 1300);
            }

            
            //Console.WriteLine("Width: {0}, Height: {1}", img.Width, img.Height);
            //Image<Gray, byte> bw = new Image<Gray, byte>(filename);
            Image<Gray, byte> bw = img.Convert<Gray, byte>();
            //bw = bw.ThresholdBinary(new Gray(150), new Gray(255));
            bw = bw.ThresholdBinaryInv(new Gray((int)this.threshold_selector.Value), new Gray(255));

            CvInvoke.Imwrite("binary.bmp", bw);
            this.pictureBox1.Image = new Bitmap("binary.bmp");

            Mat element = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(5,1), new Point(-1, -1));

            Mat element1 = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(1,5), new Point(-1, -1));
            //CvInvoke.Imshow("bw", bw);

            CvInvoke.Erode(bw
, bw
, element1
, new Point(-1, -1)
, 2
, BorderType.Constant
, new MCvScalar(255, 255, 255));
            CvInvoke.Imshow("After Erode", bw);

            CvInvoke.Dilate(bw
, bw
, element
, new Point(-1, -1)
, 2
, BorderType.Constant
, new MCvScalar(255, 255, 255));

           




            CvInvoke.Imshow("after both", bw);
            CvInvoke.WaitKey(0);

            GC.Collect();
            Emgu.CV.Util.VectorOfVectorOfPoint contours = new Emgu.CV.Util.VectorOfVectorOfPoint();
            Mat hier = new Mat();

            CvInvoke.FindContours(bw, contours, hier, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
            //CvInvoke.DrawContours(img, contours, -1, new MCvScalar(255, 0, 0),3);
            Console.WriteLine("Total contours: {0}",contours.Size);
            //CvInvoke.DrawContours(img, contours, -1, new MCvScalar(0, 0, 255), 2);
            //CvInvoke.Imshow("cnts", img);
            //CvInvoke.Imwrite("E:\\contours_image.png", img);
            //CvInvoke.WaitKey(0);
            int count = contours.Size;
            CvInvoke.Imwrite("E:\\resized_output.jpg", img);

            List<int[]> rects = remove_corner_rect(contours,img.Width,img.Height);
            rects = filter_rectangles_list(rects);


            Image<Bgr, byte> temp_image = new Image<Bgr, byte>("E:\\resized_output.jpg");

            int x_min, y_min, x_max, y_max;
            x_max = 0;
            y_max = 0;
            x_min = img.Width;
            y_min = img.Height;
            double margin_percentage = 0.001; //1% margin from all sides. (as some noise shapes appear on the edges of the images usually.)

            var sorted = (from item in rects
                          orderby (item[0] + (item[1] + item[3]) * 100)
                          select item).ToList();



            Console.WriteLine("Initial params: {0}, {1}, {2}, {3}", x_max, y_max, x_min, y_min);

            for (int i = 0; i < sorted.Count; i++)
            {
                Rectangle r = new Rectangle();
                r.X = sorted[i][0];
                r.Y = sorted[i][1];
                r.Width = sorted[i][2];
                r.Height = sorted[i][3];



                if (r.X != 0 && r.Width != 0 && r.Height * r.Width > 200 && r.X > margin_percentage * img.Width && r.Y > margin_percentage * img.Height && !(r.Height>7*r.Width || r.Width > 7*r.Height) )
                {
                    if (r.X < x_min)
                    {
                        x_min = r.X;
                    }
                    if (r.Y < y_min)
                    {
                        y_min = r.Y;
                    }

                    if (r.X + r.Width > x_max)
                    {
                        x_max = r.X + r.Width;
                    }
                    if (r.Y + r.Height > y_max)
                    {

                        y_max = r.Y + r.Height;
                    }


                    CvInvoke.Rectangle(img, r, new MCvScalar(0, 0, 255), 1);
                    Image<Bgr, byte> temp = temp_image.CopyBlank();
                    temp_image.CopyTo(temp);

                    temp.ROI = r;
                    //CvInvoke.Imshow("Image", temp);
                    string path = String.Format("{0}\\resized_output{1}.jpg", output_path, img_counter);
                    CvInvoke.Imwrite(path, temp);
                    img_counter++;
                    //Console.WriteLine("{0} Images saved...", img_counter);
                    CvInvoke.Imshow("rects.", img);
                    CvInvoke.WaitKey(100);
                }

                //CvInvoke.WaitKey(0);
            }
            GC.Collect();
            Rectangle bounding_box = new Rectangle();
            bounding_box.X = x_min;
            bounding_box.Y = y_min;
            bounding_box.Width = (x_max - x_min);
            bounding_box.Height = (y_max - y_min);
            
            Console.WriteLine("Bounding box: {0}, {1}, {2}, {3}", bounding_box.X, bounding_box.Y, bounding_box.Width, bounding_box.Height);
            CvInvoke.Rectangle(img, bounding_box, new MCvScalar(0, 0, 255), 4);

            GC.Collect();
            String bounding_img_path = String.Format("{0}\\boundingbox_image.jpg", output_path);
            CvInvoke.Imwrite("E:\\bounding_image.png", img);
            CvInvoke.Imshow("Image", img);
            CvInvoke.WaitKey(0);
            GC.Collect();

        }
        private void process_image(string filename)
        {
            GC.Collect();
            Image<Bgr, byte> img = new Image<Bgr, byte>(filename);
            
            if (img.Width < 1200) {
                img = resize_image(img, 1300);
            }
            
            //Console.WriteLine("Width: {0}, Height: {1}", img.Width, img.Height);
            //Image<Gray, byte> bw = new Image<Gray, byte>(filename);
            Image<Gray, byte> bw = img.Convert<Gray, byte>();
            //bw = bw.ThresholdBinary(new Gray(150), new Gray(255));
            bw = bw.ThresholdBinaryInv(new Gray((int)this.threshold_selector.Value), new Gray(255));

            CvInvoke.Imwrite("binary.bmp", bw);
            this.pictureBox1.Image = new Bitmap("binary.bmp");

            Mat element = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(3,3), new Point(-1, -1));

            Mat element1 = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(1,5), new Point(-1, -1));
            //CvInvoke.Imshow("bw", bw);



            CvInvoke.Dilate(bw
      , bw
      , element
      , new Point(-1, -1)
      , 1
      , BorderType.Constant
      , new MCvScalar(255, 255, 255));


            CvInvoke.Erode(bw
      , bw
      , element
      , new Point(-1, -1)
      , 1
      , BorderType.Constant
      , new MCvScalar(255, 255, 255));


            //CvInvoke.Imshow("After erode", bw);





            //CvInvoke.Imshow("after both", bw);
            //CvInvoke.WaitKey(0);
            GC.Collect();
            Emgu.CV.Util.VectorOfVectorOfPoint contours = new Emgu.CV.Util.VectorOfVectorOfPoint();
            Mat hier = new Mat();

            CvInvoke.FindContours(bw, contours, hier, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
            //CvInvoke.DrawContours(img, contours, -1, new MCvScalar(255, 0, 0),3);

            int count = contours.Size;
            bw = bw.ThresholdBinaryInv(new Gray((int)this.threshold_selector.Value), new Gray(255));
            CvInvoke.Imwrite("E:\\binarized_image.jpg", bw);
            CvInvoke.Imwrite("E:\\resized_output.jpg", img);
            List<int[]> rects = remove_corner_rect(contours, img.Width, img.Height);


            rects = filter_rectangles_list(rects);

            rects = size_filter(rects);
            Image<Bgr, byte> temp_image = new Image<Bgr, byte>("E:\\resized_output.jpg");
            
            int x_min,y_min,x_max, y_max;
            x_max = 0;
            y_max = 0;
            x_min = img.Width;
            y_min = img.Height;
            double margin_percentage = 0.001; //1% margin from all sides. (as some noise shapes appear on the edges of the images usually.)

            /*var sorted = (from item in rects
                         orderby (item[0] + ((item[1]+item[3]) )*100)
                         select item).ToList();
            */
            var sorted = (from item in rects
                          orderby (item[1]) ascending
                          select item).ToList();
            //List<int[]> final_sorted_boxes = sort_everything(sorted);
            sorted = sort_everything(sorted);
            Console.WriteLine("Initial params: {0}, {1}, {2}, {3}",x_max,y_max,x_min,y_min);

            for (int i = 0; i < sorted.Count; i++)
            {
                Rectangle r = new Rectangle();
                r.X = sorted[i][0];
                r.Y = sorted[i][1];
                r.Width = sorted[i][2];
                r.Height = sorted[i][3];



                if (r.X != 0 && r.Width != 0 && r.Height * r.Width > 200 && r.X > margin_percentage * img.Width && r.Y > margin_percentage * img.Height && !(r.Height > 7 * r.Width || r.Width > 7 * r.Height))
                {   
                    if(r.X < x_min)
                    {
                        x_min = r.X;
                    }
                    if(r.Y < y_min)
                    {
                        y_min = r.Y;
                    }

                    if (r.X + r.Width > x_max)
                    {
                        x_max=r.X+r.Width;
                    }
                    if(r.Y + r.Height> y_max)
                    {
                        
                        y_max = r.Y+r.Height;
                    }


                    CvInvoke.Rectangle(img, r, new MCvScalar(0, 0, 255), 1);
                    Image<Bgr, byte> temp = temp_image.CopyBlank();
                    temp_image.CopyTo(temp);

                    temp.ROI = r;
                    //CvInvoke.Imshow("Image", temp);
                    string path = String.Format("{0}\\resized_output{1}.jpg",output_path, img_counter);
                    CvInvoke.Imwrite(path, temp);
                    img_counter++;
                    //Console.WriteLine("{0} Images saved...", img_counter);

                    Image<Bgr, byte> resized_output = img.CopyBlank();
                    img.CopyTo(resized_output);

                    resized_output = resize_image(resized_output, 1300);
                    CvInvoke.Imshow("rects.", img);
                    CvInvoke.WaitKey(50);
                }

                //CvInvoke.WaitKey(0);
            }
            GC.Collect();
            Rectangle bounding_box = new Rectangle();
            bounding_box.X = x_min;
            bounding_box.Y = y_min;
            bounding_box.Width = (x_max - x_min);
            bounding_box.Height = (y_max - y_min);

            Console.WriteLine("Bounding box: {0}, {1}, {2}, {3}",bounding_box.X, bounding_box.Y, bounding_box.Width, bounding_box.Height);
            CvInvoke.Rectangle(img, bounding_box, new MCvScalar(0, 0, 255), 4);

            GC.Collect();
            String bounding_img_path = String.Format("{0}\\boundingbox_image.jpg", output_path);
            CvInvoke.Imwrite("E:\\bounding_image.png", img);
            CvInvoke.Imshow("Image", img);
            CvInvoke.WaitKey(0);
            GC.Collect();

        }
        
        

        
        private void browse_input_Click(object sender, EventArgs e)
        {
            GC.Collect();
            status_label.Text = "Idle...";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images (*.BMP;*.JPG;;*.JPEG;*.PNG;*.GIF)|*.BMP;*.JPEG;*.PNG;*.JPG;*.GIF|" +
        "All files (*.*)|*.*";
            ofd.Title = "Browse Input Images";
            ofd.Multiselect = true;
            images_list.Clear();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine(ofd.FileNames.Length);
                for (int i = 0;i< ofd.FileNames.Length; i++)
                {
                    images_list.Add(ofd.FileNames[i]);

                }
            }

            input_path_box.Text = ofd.FileName+"(and others)";
        }

        private void output_browse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Output path";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                output_path = fbd.SelectedPath;
                output_path_box.Text = output_path;
            }
        }

        private void update_screen()
        {
            GC.Collect();
            status_label.Text = "Processing....";
            Console.WriteLine(status_label.Text);
        }
        private void start_button_Click(object sender, EventArgs e)
        {
            GC.Collect();
            img_counter = 0;
            update_screen();

            int total_images = images_list.Count;
            if (total_images == 0)
            {
                MessageBox.Show("No image selected..", "Oops..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                status_label.Text = "Idle...";
            }
            else
            {
                for (int i = 0; i < total_images; i++)
                {
                    GC.Collect();
                    string path = images_list[i];
                    string status = string.Format("Processing image {0}/{1}", i + 1, total_images);
                    Console.WriteLine(status);
                    status_label.Text = status;
                    progress_bar.Value = (int)((float)(i + 1) * 100 / total_images);
                    try
                    {
                        process_image(path);
                    }
                    catch (Exception ex)
                    {
                        string message = string.Format("Some issue occured with: {0}\n Error: {1}", images_list[i], ex.Message);
                        MessageBox.Show("Some issue occured with ", "Oops..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }



                }

                status_label.Text = "All done! :)";
            }


        }

        private void split_words_Click(object sender, EventArgs e)
        {
            GC.Collect();
            img_counter = 0;
            update_screen();

            int total_images = images_list.Count;
            if (total_images == 0)
            {
                MessageBox.Show("No image selected..", "Oops..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                status_label.Text = "Idle...";
            }
            else
            {
                for (int i = 0; i < total_images; i++)
                {
                    GC.Collect();
                    string path = images_list[i];
                    string status = string.Format("Processing image {0}/{1}", i + 1, total_images);
                    Console.WriteLine(status);
                    status_label.Text = status;
                    progress_bar.Value = (int)((float)(i + 1) * 100 / total_images);
                    try
                    {
                        find_words(path);
                    }
                    catch (Exception ex)
                    {
                        string message = string.Format("Some issue occured with: {0}\n Error: {1}", images_list[i], ex.Message);
                        MessageBox.Show("Some issue occured with ", "Oops..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }



                }

                status_label.Text = "All done! :)";
            }
        
        
        
        }
    }
}
