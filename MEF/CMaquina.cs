using System;
using System.Drawing;
using System.IO;
using System.Reflection;


namespace MEF
{
	// Estructura usada para los objetos y la bateria
	public struct S_objeto
	{
		public bool activo;	// Indica si el objeto es visible o no
		public int x,y;		// Coordenadas del objeto
	}


	/// <summary>
	/// Esta clase representa a nuestra maquina de estados finitos.
	/// </summary>
	public class CMaquina
	{
		// Enumeracion de los diferentes estados
		public enum  estados
		{
			DORMIR, //0
            COMER, //1 
            DIVERTIRSE, //2
            TRASLADARSE, //3
            MUERTO //4
		};

		// Esta variable representa el estado actual de la maquina
		private int Estado;

		// Estas variables son las coordenadas del robot
		private int x,y;

		// Arreglo para guardar una copia de los objetos
        private S_objeto cama;
        private S_objeto cocina;
        private S_objeto sofa;

        //Variable para dormir 
        private int sueno;

        //Variable para hambre
        private int hambre;

        //Arreglo de imagenes 
        private Image[] imagenes = new Image[10];

        private int imagenIndex; //llave de acceso a la imagen


        //CONSTANTES DE TRANSICION
        const int MAX_SUENO = 3000;
        const int MAX_HAMBRE = 1500;
        const int MIN_HAMBRE= 800;
        const int MIN_SUENO = 1000;


		// Creamos las propiedades necesarias
		public int CoordX 
		{
			get {return x;}
		}

		public int CoordY
		{
			get {return y;}
		}

		public int EstadoM
		{
			get {return Estado;}
		}

        public int SuenoM { get { return sueno; } }

        public int HambreM { get { return hambre; } }

        public CMaquina()
        {
            // Este es el contructor de la clase

            // Inicializamos las variables

			Estado=(int)estados.TRASLADARSE;	// Colocamos el estado de inicio.
			x=320;		// Coordenada X
			y=240;		// Coordenada Y
			//indice=-1;	// Empezamos como si no hubiera objeto a buscar
			sueno = new Random().Next(MAX_SUENO/2, MAX_SUENO+1);
            hambre = new Random().Next(MAX_HAMBRE / 2, MAX_HAMBRE + 1); ;
		}

		public void Inicializa(S_objeto cama, S_objeto cocina, S_objeto sofa)
		{
			// Colocamos una copia de los objetos y la bateria
			// para pode trabajar internamente con la informacion

			this.cama = cama;
            this.cocina = cocina;
            this.sofa = sofa;

            //cargamos las imagenes
            string currentDirectory = Directory.GetParent(Directory.GetParent(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)).FullName).FullName;
            
            imagenes[0] = Image.FromFile(Path.Combine(currentDirectory, "assets", "caminando.png")); //caminando
            imagenes[1] = Image.FromFile(Path.Combine(currentDirectory, "assets", "dormido.png")); //dormido
            imagenes[2] = Image.FromFile(Path.Combine(currentDirectory, "assets", "muerto.png")); //muerto
            imagenes[3] = Image.FromFile(Path.Combine(currentDirectory, "assets", "comiendo.png")); //comiendo
            imagenes[4] = Image.FromFile(Path.Combine(currentDirectory, "assets", "comiendo1.png")); //comiendo
            imagenes[5] = Image.FromFile(Path.Combine(currentDirectory, "assets", "comiendo2.png")); //comiendo
            imagenes[6] = Image.FromFile(Path.Combine(currentDirectory, "assets", "ninja.png")); //entretenido
            imagenes[7] = Image.FromFile(Path.Combine(currentDirectory, "assets", "chrome.png")); //entretenido
            imagenes[8] = Image.FromFile(Path.Combine(currentDirectory, "assets", "cool.png")); //entretenido
            imagenes[9] = Image.FromFile(Path.Combine(currentDirectory, "assets", "bailando.png")); //entretenido
		}

        public Image getImagen() {
            return imagenes[imagenIndex];
        }

		public void Control()
		{
			// Esta funcion controla la logica principal de la maquina de estados
			switch(Estado)
			{
				case ( int ) estados.TRASLADARSE:
                    TRASLADARSE();
                    imagenIndex = 0;

                    if (sueno < 0 || hambre < 0)
                    {
                        imagenIndex = 2; 
                        Estado = (int)estados.MUERTO;
                    }

                    //moverse
                    if (x == cama.x && y == cama.y)
                    {
                        imagenIndex = 1;
                        Estado = (int)estados.DORMIR;
                    }

                    if (x == cocina.x && y == cocina.y)
                    {
                        imagenIndex = new Random().Next(3, 6);
                        Estado = (int)estados.COMER;
                    }

                    if (x == sofa.x && y == sofa.y)
                    {
                        imagenIndex = new Random().Next(6, 10);
                        Estado = (int)estados.DIVERTIRSE;
                    }

                    break;

                case (int)estados.DORMIR:

                    DORMIR();

                    if (sueno >= MAX_SUENO)
                    {

                        Estado = (int)estados.TRASLADARSE;
                    }

                    break;

                case (int)estados.COMER:

                    COMER();

                    if (hambre >= MAX_HAMBRE || sueno < MIN_SUENO)
                        Estado = (int)estados.TRASLADARSE;

                    break;

                case (int)estados.DIVERTIRSE:

                    DIVERTIRSE();

                    if (hambre < MIN_HAMBRE || sueno < MIN_SUENO)
                        Estado = (int)estados.TRASLADARSE;


                    break;

                case (int)estados.MUERTO:

                    //NO HAY TRANSICION

                    break;



			}

		}

        private void TRASLADARSE() {
            //ir a la cama SI TIENE SUEÑO
            if (sueno < MIN_SUENO) //tiene sueno
            {
                //moverse
                if (x < cama.x)
                    x++;
                else if (x > cama.x)
                    x--;

                if (y < cama.y && x== cama.x)
                    y++;
                else if (y > cama.y && x==cama.x)
                    y--;

            }
            //ir a la cocina SI TIENE HAMBRE Y NO TIENE SUEÑO
            else if (hambre < MIN_HAMBRE)
            {
                //moverse
                if (x < cocina.x && y == cocina.y)
                    x++;
                else if (x > cocina.x && y== cocina.y)
                    x--;

                if (y < cocina.y)
                    y++;
                else if (y > cocina.y)
                    y--;

            }
            //ir a la radio SI NO TIENE SUEÑO NI HAMBRE
            else
            {
                //moverse
                if (x < sofa.x && y==sofa.y)
                    x++;
                else if (x > sofa.x && y==sofa.y)
                    x--;

                if (y < sofa.y)
                    y++;
                else if (y > sofa.y)
                    y--;
            }

            //decrementar sueno y hambre
            sueno--;
            hambre--;
        }

        private void DORMIR() {
            sueno+=4;
            //hambre--;
        }

        private void COMER() {
            hambre+=2;
            sueno--;
        }

        private void DIVERTIRSE() {
           sueno--;
           hambre--;
        }


	}
}
