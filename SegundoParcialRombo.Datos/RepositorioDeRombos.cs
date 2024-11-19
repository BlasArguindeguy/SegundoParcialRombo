using SegundoParcialRombo.Entidades;

namespace SegundoParcialRombo.Datos
{
    public class RepositorioDeRombos
    {
        private readonly string _archivo = Environment.CurrentDirectory + "//Rombos.txt";
        private readonly string _archivoCopia = Environment.CurrentDirectory + "//Rombos.bak";

        private List<Rombo> listaRombo;
        public RepositorioDeRombos()
        {
            listaRombo = new List<Rombo>();
            LeerDatos();
        }

        private void LeerDatos()
        {
            listaRombo.Clear();
            if (File.Exists(_archivo))
            {
                var lector = new StreamReader(_archivo);
                while (!lector.EndOfStream)
                {
                    string lineaLeida = lector.ReadLine();
                    Rombo rombo = ConstruirRombo(lineaLeida);
                    listaRombo.Add(rombo);
                }
                lector.Close();
            }
        }

        public void Editar(Rombo romboViejo, Rombo romboEditar)
        {
            using (var lector = new StreamReader(_archivo))
            {
                using (var escritor = new StreamWriter(_archivoCopia))
                {
                    while (!lector.EndOfStream)
                    {
                        string lineaLeida = lector.ReadLine();
                        Rombo rombo = ConstruirRombo(lineaLeida);
                        if (rombo.GetArea() == romboViejo.GetArea() &&
                            rombo.Contorno.GetHashCode() == romboViejo.Contorno.GetHashCode())
                        {
                            lineaLeida = ConstruirLinea(romboEditar);
                            escritor.WriteLine(lineaLeida);
                        }
                        else
                        {
                            escritor.WriteLine(lineaLeida);

                        }
                    }
                }
            }
            File.Delete(_archivo);
            File.Move(_archivoCopia, _archivo);
        }
        private Rombo ConstruirRombo(string lineaLeida)
        {

            var campos = lineaLeida.Split('|');
            int diagonalMayor = int.Parse(campos[0]);
            int diagonalMenor = int.Parse(campos[1]);
            Contorno borde = (Contorno)Enum.Parse(typeof(Contorno), campos[2]);
            Rombo rombo = new Rombo(diagonalMayor, diagonalMenor, borde);
            return rombo;
        }

        public void Agregar(Rombo rombo)
        {
            using (var escritor = new StreamWriter(_archivo, true))
            {
                string lineaEscribir = ConstruirLinea(rombo);
                escritor.WriteLine(lineaEscribir);
            }
            listaRombo.Add(rombo);
        }

        private string ConstruirLinea(Rombo rombo)
        {
            return $"{rombo.GetMayor()}|" +
                   $"{rombo.GetMenor()}|" +
                   $"{rombo.Contorno.GetHashCode()}";
        }

        public int GetCantidad(int? valorFiltro = 0)
        {
            if (valorFiltro > 0)
            {
                return listaRombo.Count(c => c.DiagonalMayor > valorFiltro);
            }
            return listaRombo.Count();
        }

        public void Borrar(Rombo romboBorrar)
        {
            using (var lector = new StreamReader(_archivo))
            {
                using (var escritor = new StreamWriter(_archivoCopia))
                {
                    while (!lector.EndOfStream)
                    {
                        string lineaLeida = lector.ReadLine();
                        Rombo romboLeido = ConstruirRombo(lineaLeida);
                        if (romboBorrar.GetArea() != romboLeido.GetArea())
                        {
                            escritor.WriteLine(lineaLeida);
                        }
                    }
                }
            }
            File.Delete(_archivo);
            File.Move(_archivoCopia, _archivo);
            listaRombo.Remove(romboBorrar);
        }
        public List<Rombo> GetLista()
        {
            LeerDatos();
            return listaRombo;
        }

        public List<Rombo> Filtrar(int valorFiltro)
        {
            return listaRombo.Where(l => l.Contorno.GetHashCode() == valorFiltro).ToList();
        }

        //ORDENAR
        public List<Rombo> OrdenarAscArista()
        {
            return listaRombo.OrderBy(l => l.GetArea()).ToList();
        }

        public List<Rombo> OrdenarDescArista()
        {
            return listaRombo.OrderByDescending(l => l.GetArea()).ToList();
        }


        public bool Existe(Rombo rombo)
        {
            listaRombo.Clear();
            LeerDatos();
            bool existe = false;
            foreach (var itemRombo in listaRombo)
            {
                if (itemRombo.GetArea() == rombo.GetArea() &&
                    itemRombo.Contorno == rombo.Contorno)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
