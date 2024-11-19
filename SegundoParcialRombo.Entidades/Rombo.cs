namespace SegundoParcialRombo.Entidades
{
    public class Rombo
    {
        public int DiagonalMayor { get; set; }
        public int DiagonalMenor { get; set; }
        public double Lado
        {
            get
            {

                return Math.Sqrt(Math.Pow(DiagonalMayor / 2.0, 2) + Math.Pow(DiagonalMenor / 2.0, 2));
            }
        }
        private Contorno contorno;
        public Contorno Contorno
        {
            get { return contorno; }
            set { contorno = value; }
        }

        public Rombo()
        {
        }

        public Rombo(int diagonalMayor, int diagonalMenor, Contorno contorno)
        {
            DiagonalMayor = diagonalMayor;
            DiagonalMenor = diagonalMenor;
            Contorno = contorno;
        }

        public double GetArea()
        {

            return (DiagonalMayor * DiagonalMenor) / 2.0;
        }
        public double GetPerimetro()
        {

            return 4 * Lado;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public int GetMayor()
        {
            return DiagonalMayor;
        }
        public int GetMenor()
        {
            return DiagonalMenor;
        }
        public double GetLado()
        {
            return Lado;
        }

        public void SetDiagonalMenor(int medida)
        {
            if (medida > 0)
            {
                DiagonalMenor = medida;
            }
            else
            {
                throw new ArgumentException("La diagonal menor debe ser mayor a 0.");
            }
        }
        public void SetDiagonalMayor(int medida)
        {
            if (medida <= 0)
            {
                throw new ArgumentException("La diagonal mayor debe ser mayor a 0.");
            }

            if (DiagonalMenor != 0 && medida < DiagonalMenor)
            {
                throw new ArgumentException("La diagonal mayor no puede ser menor que la diagonal menor.");
            }

            DiagonalMayor = medida;
        }
    }
}
