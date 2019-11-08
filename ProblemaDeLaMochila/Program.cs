using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemaDeLaMochila
{
    class Program
    {
        static void Main(string[] args)
        {

            int capacidadMochila = 3;

            List<ObjetosAEvaluar> misObjetos = new List<ObjetosAEvaluar>();

            misObjetos.Add(new ObjetosAEvaluar("A",1,1));
            misObjetos.Add(new ObjetosAEvaluar("B", 1, 2));
            misObjetos.Add(new ObjetosAEvaluar("C", 2, 5));

            int [,] Arreglo = new int [capacidadMochila+1,misObjetos.Count()+1];
            //for (int i=0; i<=capacidadMochila; i++)
            //{
            //    //for (int x =0; x<misObjetos.Count(); x++)
            //    //{
            //      //  Arreglo[i, x]=ValorCasilla(x,misObjetos,i);
            //        //Console.Write(Arreglo[i,x]);
            //    //}
            //    Console.WriteLine(Arreglo[0,i]);
            //}


            for (int fila = 0; fila<=misObjetos.Count; fila++)
            {
                for (int columna = 0; columna<=capacidadMochila; columna++)
                {
                    Arreglo[fila, columna]=CalcularValor(fila,columna,misObjetos);
                    Console.Write(Arreglo[fila, columna]);
                }
             
                Console.WriteLine();
            }

            Console.ReadLine();
        }

        private static int CalcularValor(int fila, int columna, List<ObjetosAEvaluar> misObjetos)
        {
            int counterFila = 0;
            int counterColumna = 0;
            int valorDeCelda = 0;
            int pesoAcumulado = 0;

            if (fila==0)
            {
                return 0;
            }
            if (columna==0)
            {
                return 0;
            }

            //If the row = 1: We have to take the highest value
            //If the row > 1: We have to take the highest combination of values within the current object limit

            if (fila==1 || columna ==1)
            {
                int highestValue = 0;
                for (int i = 0; i<fila; i++)
                {
                    if (misObjetos[i].peso<=1)
                    {
                        if (highestValue<=misObjetos[i].valor)
                        {
                            highestValue=misObjetos[i].valor;
                        }
                    }
                    valorDeCelda=highestValue;
                }
            }
            else
            {
                int highestCombinationValue = 0;
                int historical = 0;
                for (int i = 0; i<fila; i++)
                {
                    if((misObjetos[i].peso+pesoAcumulado)<=columna)
                    {
                        
                        highestCombinationValue+=misObjetos[i].valor;
                        pesoAcumulado+=misObjetos[i].peso;
                    }
                }

                pesoAcumulado=0;
                for (int i = fila-1; i >=0; i --)
                {
                    if ((misObjetos[i].peso+pesoAcumulado)<=columna)
                    {
                        historical+=misObjetos[i].valor;
                        pesoAcumulado+=misObjetos[i].peso;
                    }
                }

                if (highestCombinationValue<historical)
                    valorDeCelda=historical;
                else
                    valorDeCelda=highestCombinationValue;

                //valorDeCelda=highestCombinationValue;

            }
           
            



            return valorDeCelda;
        }

        public static int  ValorCasilla(int capacitad, List<ObjetosAEvaluar> misObjetos, int filaDeLaTabla)
        {
            int R = 0, T=capacitad;
            int counter = 0;

            foreach (var item in misObjetos)
            {
                if (item.peso<=T)
                {
                    if (item.valor>=R) {
                        R=item.valor;
                        T=T-item.peso;
                    }
                }
                counter+=1;
                if (counter>filaDeLaTabla)
                    break;
            }

            return R ;
        }
    }

    

    public class ObjetosAEvaluar
    {
        public string nombre { get; set; }
        public int peso { get; set; }
        public int valor { get; set; }


        public ObjetosAEvaluar(string name,int weight,int value)
        {
            nombre=name;
            peso=weight;
            valor=value;
        }

    }
}
