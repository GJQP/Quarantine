using System;


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
        private S_objeto radio;

        //Variable para dormir 
        private int sueno;

        //Variable para hambre
        private int hambre;

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
			
		public CMaquina()
		{
			// Este es el contructor de la clase

			// Inicializamos las variables

			Estado=(int)estados.TRASLADARSE;	// Colocamos el estado de inicio.
			x=320;		// Coordenada X
			y=240;		// Coordenada Y
			//indice=-1;	// Empezamos como si no hubiera objeto a buscar
			sueno = 400;
            hambre = 100;
		}

		public void Inicializa(S_objeto cama, S_objeto cocina, S_objeto radio)
		{
			// Colocamos una copia de los objetos y la bateria
			// para pode trabajar internamente con la informacion

			this.cama = cama;
            this.cocina = cocina;
            this.radio = radio;

		}

		public void Control()
		{
			// Esta funcion controla la logica principal de la maquina de estados
			
			switch(Estado)
			{
				case ( int ) estados.TRASLADARSE:
                    TRASLADARSE();

                    
                    if (sueno < 0 || hambre < 0)
                        Estado = (int)estados.MUERTO;

                    //moverse
                    if (x == cama.x && y == cama.y)
                        Estado = (int)estados.DORMIR;

                    if (x == cocina.x && y == cocina.y)
                        Estado = (int)estados.COMER;

                    if (x == radio.x && y == radio.y)
                        Estado = (int)estados.DIVERTIRSE;
                       

                    break;

                case (int)estados.DORMIR:

                    DORMIR();

                    if (sueno == 400)
                        Estado = (int)estados.TRASLADARSE;

                    break;

                case (int)estados.COMER:

                    COMER();

                    if (hambre == 100 || sueno < 40)
                        Estado = (int)estados.TRASLADARSE;

                    break;

                case (int)estados.DIVERTIRSE:

                    DIVERTIRSE();

                    if (hambre < 10 || sueno < 40)
                        Estado = (int)estados.TRASLADARSE;


                    break;

                case (int)estados.MUERTO:

                    //NO HAY TRANSICION

                    break;



			}

		}

        private void TRASLADARSE() {

            //ir a la cama SI TIENE SUEÑO
            if (sueno < 40) //tiene sueno
            {
                //moverse
                if (x < cama.x)
                    x++;
                else if (x > cama.x)
                    x--;

                if (y < cama.y)
                    y++;
                else if (y > cama.y)
                    y--;

            }
            //ir a la cocina SI TIENE HAMBRE Y NO TIENE SUEÑO
            else if (hambre < 10)
            {
                //moverse
                if (x < cocina.x)
                    x++;
                else if (x > cocina.x)
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
                if (x < radio.x)
                    x++;
                else if (x > radio.x)
                    x--;

                if (y < radio.y)
                    y++;
                else if (y > radio.y)
                    y--;
            }

            //decrementar sueno y hambre
            sueno--;
            hambre--;
        }

        private void DORMIR() {
            sueno++;
            hambre--;
        }

        private void COMER() {
            hambre++;
            sueno--;
        }

        private void DIVERTIRSE() { 
           sueno--;
           hambre--;
        }


	}
}
