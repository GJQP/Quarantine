using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Reflection;

namespace MEF
{
    
    public class Form1 : Form
    {
        private MainMenu mainMenu1;
        private MenuItem menuItem1;
        private MenuItem mnuSalir;
        private MenuItem menuItem3;
        private MenuItem mnuInicio;
        private MenuItem mnuParo;
        private Timer timer1;
        private IContainer components;

        // Creamos un objeto para la maquina de estados finitos
        private CMaquina maquina = new CMaquina();

        // Objetos necesarios
        public S_objeto[] ListaObjetos = new S_objeto[10];
        public S_objeto MiCama;
        public S_objeto MiCocina;
        public S_objeto MiRadio;

        //TEST
        string currentDirectory;
        string assetsFolder;
        string yoshiImgPath;
        public Image yoshi;
        public Image backgroundImg;
        public Form1()
        {
            //
            // Necesario para admitir el Diseñador de Windows Forms
            //
            InitializeComponent();

            //
            // TODO: agregar código de constructor después de llamar a InitializeComponent
            //

            
            // Inicializamos el dir actual
            currentDirectory = Directory.GetParent(Directory.GetParent(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)).FullName).FullName;
            yoshiImgPath = Path.Combine(currentDirectory, "assets", "yoshi.jpg");
            yoshi = Image.FromFile(yoshiImgPath);

          
            //colocar Background
            this.BackgroundImage = Image.FromFile(Path.Combine(currentDirectory, "assets", "background.png"));

            // colocar para que no se repita la imagen
            this.BackgroundImageLayout = ImageLayout.Stretch;
            

            // Inicializamos los objetos

            // Cremos un objeto para tener valores aleatorios
            Random random = new Random();

            // Colocamos la cama
            MiCama.x = 300;
            MiCama.y = 105;
            MiCama.activo = true;

            // Colocamos la cocina
            MiCocina.x = 150;
            MiCocina.y = 330;
            MiCocina.activo = true;

            // Colocamos la radio
            MiRadio.x = 530;
            MiRadio.y = 365;
            MiRadio.activo = true;

            maquina.Inicializa(MiCama,MiCocina,MiRadio);


        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms
        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.mnuSalir = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.mnuInicio = new System.Windows.Forms.MenuItem();
            this.mnuParo = new System.Windows.Forms.MenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem3});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuSalir});
            this.menuItem1.Text = "Archivo";
            // 
            // mnuSalir
            // 
            this.mnuSalir.Index = 0;
            this.mnuSalir.Text = "Salir";
            this.mnuSalir.Click += new System.EventHandler(this.mnuSalir_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuInicio,
            this.mnuParo});
            this.menuItem3.Text = "Aplicacion";
            // 
            // mnuInicio
            // 
            this.mnuInicio.Index = 0;
            this.mnuInicio.Text = "Inicio";
            this.mnuInicio.Click += new System.EventHandler(this.mnuInicio_Click);
            // 
            // mnuParo
            // 
            this.mnuParo.Index = 1;
            this.mnuParo.Text = "Paro";
            this.mnuParo.Click += new System.EventHandler(this.mnuParo_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 5;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(692, 500);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Maquina de estados finitos";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// Punto de entrada principal de la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new Form1());
        }

        private void mnuSalir_Click(object sender, System.EventArgs e)
        {
            // Cerramos la ventana y finalizamos la aplicacion
            this.Close();
        }

        private void mnuInicio_Click(object sender, System.EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void mnuParo_Click(object sender, System.EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            // Esta funcion es el handler del timer
            // Aqui tendremos la logica para actualizar nuestra maquina de estados

            // Actualizamos a la maquina
            maquina.Control();

            // Mandamos a redibujar la pantalla
            this.Invalidate();
        }

        private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // Creamos la fuente y la brocha para el texto
            Font fuente = new Font("Arial", 16);
            SolidBrush brocha = new SolidBrush(Color.White);
           
            //e.Graphics.DrawImage AQUIIIIIIIIII
            // Dibujamos el robot
            e.Graphics.DrawImage(maquina.getImagen(), maquina.CoordX - 4, maquina.CoordY - 4, 40, 40);

            // Dibujamos la cama
            e.Graphics.DrawRectangle(Pens.IndianRed, MiCama.x - 4, MiCama.y - 4, 20, 20);

            // Dibujamos la cocina
            e.Graphics.DrawRectangle(Pens.Aqua, MiCocina.x - 4, MiCocina.y - 4, 20, 20);

            // Dibujamos la radio
            e.Graphics.DrawRectangle(Pens.Green, MiRadio.x - 4, MiRadio.y - 4, 20, 20);
            //e.Graphics.DrawImage(bailando, MiRadio.x - 4, MiRadio.y -4, 20, 20);

            // Indicamos el estado en que se encuentra la maquina
            e.Graphics.DrawString("Estado -> " + maquina.EstadoM.ToString(), fuente, brocha, 10, 10);

            e.Graphics.DrawString("Sueno -> " + maquina.SuenoM.ToString(), fuente, brocha, 10, 40);

            e.Graphics.DrawString("Hambre -> " + maquina.HambreM.ToString(), fuente, brocha, 10, 70);


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
