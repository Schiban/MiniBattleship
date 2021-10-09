using System;

namespace MiniBattleship
{
    class Program
    {
        // ■ Função - Validar Números
        static int ValidarNumero(int inicial, int final)
        {
            /*  Função que valida a entrada de valores numéricos
                    ▪ Recebe 2 parâmetros que limitam a entrada de valor
                    ▪ Devolve o valor de uma variável numérica válida 
                    ▪ Não permite a inserção de letras ou caracteres */

            // Declaração de variáveis
            bool ok = false;
            int iNum = 0;
            string sString = "";

            // Ciclo de validação da inserção de dados
            do
            {
                sString = Console.ReadLine();
                bool result = int.TryParse(sString, out iNum);
                if (!result || iNum < inicial || iNum > final)
                    Console.WriteLine($"Por favor insira um número entre {inicial} e {final}.");
                else
                    ok = true;
            }
            while (!ok);
            return iNum;
        }


        // ■ Função - Intro
        private static void Intro()
        {
            // Apenas com o propósito de dar as boas vindas ao utilizador
            Console.Write("\n■ BEM-VINDO AO MINIBATTLESHIP!\n\n");
            Console.WriteLine($"     0   1   2   3   4");
            Console.WriteLine($"   ┌───┬───┬───┬───┬───┐");
            Console.WriteLine($" 0 │   │   │   │   │ X │ ");
            Console.WriteLine($"   ├───┼───┼───┼───┼───┤       BATTLESHIP");
            Console.WriteLine($" 1 │   │ X │   │   │   │");
            Console.WriteLine($"   ├───┼───┼───┼───┼───┤           BY");
            Console.WriteLine($" 2 │   │   │   │   │   │");
            Console.WriteLine($"   ├───┼───┼───┼───┼───┤     FÁBIO OLIVEIRA");
            Console.WriteLine($" 3 │   │   │ X │ X │   │     JOÃO BANDEIRA");
            Console.WriteLine($"   ├───┼───┼───┼───┼───┤");
            Console.WriteLine($" 4 │ X │   │   │   │   │");
            Console.WriteLine($"   └───┴───┴───┴───┴───┘\n");
        }


        // ■ Função - Quadro
        private static void Quadro()
        {
            // Tem como objetivo apresentar ao utilizador o quadro inicial, de modo a ajudá-lo a indicar as coordenadass a inserir
            Console.WriteLine($"     0   1   2   3   4");
            Console.WriteLine($"   ┌───┬───┬───┬───┬───┐");
            Console.WriteLine($" 0 │   │   │   │   │   │ ");
            Console.WriteLine($"   ├───┼───┼───┼───┼───┤");
            Console.WriteLine($" 1 │   │   │   │   │   │");
            Console.WriteLine($"   ├───┼───┼───┼───┼───┤");
            Console.WriteLine($" 2 │   │   │   │   │   │");
            Console.WriteLine($"   ├───┼───┼───┼───┼───┤");
            Console.WriteLine($" 3 │   │   │   │   │   │");
            Console.WriteLine($"   ├───┼───┼───┼───┼───┤");
            Console.WriteLine($" 4 │   │   │   │   │   │");
            Console.WriteLine($"   └───┴───┴───┴───┴───┘\n");
        }


        // ■ Main
        static void Main(string[] args)
        {
            // Declaração de variáveis
            Random rnd = new Random();

            // Matriz de navios que o computador vai gerar
            int[,] naviosComputador = new int[5, 5];
            string[,] naviosComputador2 = new string[5, 5];

            // Arrays que vão ser usadas para preenchimentos
            int[] eixoxV = new int[5];
            int[] eixoyV = new int[5];

            // Matriz que o utilzador vai preencher com as suas tentativas
            int[,] naviosUser = new int[5, 5];
            string[,] naviosUser2 = new string[5, 5];

            //Matriz de resultados dos navios que coicidem com as tentativas do utilizador e a matriz gerada pelo computador
            string[,] naviosAfundados = new string[5, 5];

            // Variável para calcular o número de acertos
            int acertou = 0;


            // Ciclo para preencher a matriz "naviosComputador" com zeros
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    naviosComputador[i, j] = 0;


            // Ciclo para escolher aleatoriamente 5 posições da matriz que servirão para colocar os navios do computador
            for (int i = 0; i < 5; i++)
            {
                eixoxV[i] = rnd.Next(0, 5);
                eixoyV[i] = rnd.Next(0, 5);
                for (int j = 0; j < i; j++)
                    // Caso alguma coordenada calhe de ser igual a alguma das anteriores, é retirado 1 ao valor de "i" para recomeçar o ciclo no ponto anterior
                    if (eixoxV[i] == eixoxV[j] && eixoyV[i] == eixoyV[i])
                        i -= 1;
                /* Quando são inseridas as coordenadas e estas são diferentes da anteriores, então a matriz "naviosComputador" substitui o 0 inicial por 1.
                    ▪ 0 = Espaço vazio (Mar)
                    ▪ 1 = Navio */
                naviosComputador[eixoxV[i], eixoyV[i]] = 1;
            }

            /* Ciclo para marcar os navios do computador
                ▪ 0 = Espaço vazio (Mar)
                ▪ X = Navio */
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (naviosComputador[i, j] == 0)
                    {
                        naviosComputador2[i, j] = " ";
                    }
                    else
                    {
                        naviosComputador2[i, j] = "X";
                    }
                }
            }


            // Apresentação do ecrã de boas vindas
            Console.Clear();
            Intro();


            // Pedido para o utilizador selecionar o número de tentativas
            Console.WriteLine("Indique o número de tentativas: ");
            int nTentativas = ValidarNumero(1, 5);
            Console.Clear();
            Quadro();


            /* Ciclo para preencher a matriz das tentativas com o valor 0
                ▪ 0 = Espaço vazio (Mar) */
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    naviosUser[i, j] = 0;


            // Arrays que vão ser usadas para preenchimentos
            int[] linha = new int[nTentativas];
            int[] coluna = new int[nTentativas];


            // Ciclo para o utilizador indicar onde quer colocar as bombas para afundar os navios do computador
            for (int i = 0; i < nTentativas; i++)
            {
                Console.WriteLine($"Tentativa nº {i + 1}:");
                Console.WriteLine("───────────────");
                Console.WriteLine("Indique a Linha:");
                linha[i] = ValidarNumero(0, 4);
                Console.WriteLine("Indique a Coluna:");
                coluna[i] = ValidarNumero(0, 4);

                // Na primeira inserção da linha/coluna, o primeiro valor é logo atribuído
                if (i == 0)
                {
                    Console.Clear();

                    // Primeira tentativa corresponde à primeira bomba, logo é substituído o 0 por 1
                    naviosUser[linha[i], coluna[i]] = 1;
                    for (int k = 0; k < 5; k++)
                    {
                        for (int l = 0; l < 5; l++)
                        {
                            // Se for 0, a matriz de string da mesma posição da matriz naviosUser é atribuído um espaço vazio
                            if (naviosUser[k, l] == 0)
                                naviosUser2[k, l] = " ";

                            // Se for 1, à matriz é atribuído um X (como bomba largada)
                            else
                                naviosUser2[k, l] = "X";
                        }
                    }

                    // Apresentação do quadro com a primeira opção escolhida
                    Console.Clear();
                    Console.WriteLine($"Linha {linha[i]} // Coluna {coluna[i]}\n");
                    Console.WriteLine($"     0   1   2   3   4 ");
                    Console.WriteLine($"   ┌───┬───┬───┬───┬───┐");
                    Console.WriteLine($" 0 │ {naviosUser2[0, 0]} │ {naviosUser2[0, 1]} │ {naviosUser2[0, 2]} │ {naviosUser2[0, 3]} │ {naviosUser2[0, 4]} │");
                    Console.WriteLine($"   ├───┼───┼───┼───┼───┤");
                    Console.WriteLine($" 1 │ {naviosUser2[1, 0]} │ {naviosUser2[1, 1]} │ {naviosUser2[1, 2]} │ {naviosUser2[1, 3]} │ {naviosUser2[1, 4]} │");
                    Console.WriteLine($"   ├───┼───┼───┼───┼───┤");
                    Console.WriteLine($" 2 │ {naviosUser2[2, 0]} │ {naviosUser2[2, 1]} │ {naviosUser2[2, 2]} │ {naviosUser2[2, 3]} │ {naviosUser2[2, 4]} │");
                    Console.WriteLine($"   ├───┼───┼───┼───┼───┤");
                    Console.WriteLine($" 3 │ {naviosUser2[3, 0]} │ {naviosUser2[3, 1]} │ {naviosUser2[3, 2]} │ {naviosUser2[3, 3]} │ {naviosUser2[3, 4]} │");
                    Console.WriteLine($"   ├───┼───┼───┼───┼───┤");
                    Console.WriteLine($" 4 │ {naviosUser2[4, 0]} │ {naviosUser2[4, 1]} │ {naviosUser2[4, 2]} │ {naviosUser2[4, 3]} │ {naviosUser2[4, 4]} │");
                    Console.WriteLine($"   └───┴───┴───┴───┴───┘");
                }

                // Ciclo correspondente às tentativas seguintes
                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                    {

                        // Ciclo para verificar caso existam coordenadas repetidas introduzidas pelo utilizador
                        if (linha[j] == linha[i] && coluna[j] == coluna[i])
                        {
                            Console.Clear();

                            // Se estas existirem, então é apresentada uma mensagem de erro e é repetido o ciclo
                            Console.WriteLine($"ERRO! Já inseriu as coordenadas [{linha[i]},{coluna[i]}].");
                            i -= 1;
                        }

                        // Ciclo que atribui com um X a escolha do utilizador
                        else
                        {
                            Console.Clear();
                            naviosUser[linha[i], coluna[i]] = 1;
                            for (int k = 0; k < 5; k++)
                                for (int l = 0; l < 5; l++)
                                {
                                    if (naviosUser[k, l] == 0)
                                        naviosUser2[k, l] = " ";
                                    else
                                        naviosUser2[k, l] = "X";
                                }
                        }
                    }

                    // Apresentação do quadro com as opções escolhidas como guia para o utilizador
                    Console.WriteLine($"     0   1   2   3   4 ");
                    Console.WriteLine($"   ┌───┬───┬───┬───┬───┐");
                    Console.WriteLine($" 0 │ {naviosUser2[0, 0]} │ {naviosUser2[0, 1]} │ {naviosUser2[0, 2]} │ {naviosUser2[0, 3]} │ {naviosUser2[0, 4]} │");
                    Console.WriteLine($"   ├───┼───┼───┼───┼───┤");
                    Console.WriteLine($" 1 │ {naviosUser2[1, 0]} │ {naviosUser2[1, 1]} │ {naviosUser2[1, 2]} │ {naviosUser2[1, 3]} │ {naviosUser2[1, 4]} │");
                    Console.WriteLine($"   ├───┼───┼───┼───┼───┤");
                    Console.WriteLine($" 2 │ {naviosUser2[2, 0]} │ {naviosUser2[2, 1]} │ {naviosUser2[2, 2]} │ {naviosUser2[2, 3]} │ {naviosUser2[2, 4]} │");
                    Console.WriteLine($"   ├───┼───┼───┼───┼───┤");
                    Console.WriteLine($" 3 │ {naviosUser2[3, 0]} │ {naviosUser2[3, 1]} │ {naviosUser2[3, 2]} │ {naviosUser2[3, 3]} │ {naviosUser2[3, 4]} │");
                    Console.WriteLine($"   ├───┼───┼───┼───┼───┤");
                    Console.WriteLine($" 4 │ {naviosUser2[4, 0]} │ {naviosUser2[4, 1]} │ {naviosUser2[4, 2]} │ {naviosUser2[4, 3]} │ {naviosUser2[4, 4]} │");
                    Console.WriteLine($"   └───┴───┴───┴───┴───┘");
                }
                Console.WriteLine("");

            }
            Console.Clear();


            // Ciclo para verificar em quantos navios o utilizador acertou
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (naviosUser[i, j] == naviosComputador[i, j] && naviosUser[i, j] == 1 && naviosComputador[i, j] == 1)
                    {
                        // Preenche um novo array com X para ser de leitura mais fácil para o utilizador
                        naviosAfundados[i, j] = "X";
                        acertou += 1;
                    }
                    else
                        naviosAfundados[i, j] = " ";
                }
            }

            /* Apresentação das tabelas do "user" e do "computador"
                ▪ Utlizador corresponde às tentativas do utilizador
                ▪ Computador corresponde às escolhas aleatórias do computador */
            Console.WriteLine("   USER                           COMPUTADOR");
            Console.WriteLine("   ─────────────────────          ─────────────────────");
            Console.WriteLine("     0   1   2   3   4              0   1   2   3   4");
            Console.WriteLine("   ┌───┬───┬───┬───┬───┐          ┌───┬───┬───┬───┬───┐");
            Console.WriteLine($" 0 │ {naviosUser2[0, 0]} │ {naviosUser2[0, 1]} │ {naviosUser2[0, 2]} │ {naviosUser2[0, 3]} │ {naviosUser2[0, 4]} │          │ {naviosComputador2[0, 0]} │ {naviosComputador2[0, 1]} │ {naviosComputador2[0, 2]} │ {naviosComputador2[0, 3]} │ {naviosComputador2[0, 4]} │ 0");
            Console.WriteLine("   ├───┼───┼───┼───┼───┤          ├───┼───┼───┼───┼───┤");
            Console.WriteLine($" 1 │ {naviosUser2[1, 0]} │ {naviosUser2[1, 1]} │ {naviosUser2[1, 2]} │ {naviosUser2[1, 3]} │ {naviosUser2[1, 4]} │          │ {naviosComputador2[1, 0]} │ {naviosComputador2[1, 1]} │ {naviosComputador2[1, 2]} │ {naviosComputador2[1, 3]} │ {naviosComputador2[1, 4]} │ 1");
            Console.WriteLine("   ├───┼───┼───┼───┼───┤          ├───┼───┼───┼───┼───┤");
            Console.WriteLine($" 2 │ {naviosUser2[2, 0]} │ {naviosUser2[2, 1]} │ {naviosUser2[2, 2]} │ {naviosUser2[2, 3]} │ {naviosUser2[2, 4]} │    VS    │ {naviosComputador2[2, 0]} │ {naviosComputador2[2, 1]} │ {naviosComputador2[2, 2]} │ {naviosComputador2[2, 3]} │ {naviosComputador2[2, 4]} │ 2");
            Console.WriteLine("   ├───┼───┼───┼───┼───┤          ├───┼───┼───┼───┼───┤");
            Console.WriteLine($" 3 │ {naviosUser2[3, 0]} │ {naviosUser2[3, 1]} │ {naviosUser2[3, 2]} │ {naviosUser2[3, 3]} │ {naviosUser2[3, 4]} │          │ {naviosComputador2[3, 0]} │ {naviosComputador2[3, 1]} │ {naviosComputador2[3, 2]} │ {naviosComputador2[3, 3]} │ {naviosComputador2[3, 4]} │ 3");
            Console.WriteLine("   ├───┼───┼───┼───┼───┤          ├───┼───┼───┼───┼───┤");
            Console.WriteLine($" 4 │ {naviosUser2[4, 0]} │ {naviosUser2[4, 1]} │ {naviosUser2[4, 2]} │ {naviosUser2[4, 3]} │ {naviosUser2[4, 4]} │          │ {naviosComputador2[4, 0]} │ {naviosComputador2[4, 1]} │ {naviosComputador2[4, 2]} │ {naviosComputador2[4, 3]} │ {naviosComputador2[4, 4]} │ 4");
            Console.WriteLine("   └───┴───┴───┴───┴───┘          └───┴───┴───┴───┴───┘");

            // Ciclo que mostra o resultado de vezes que o utilizador acertou nos navios do computador
            if (acertou > 0)
            {
                // A apresentação da mensagem muda consoante acerte em apenas 1 ou vários navios
                if (acertou == 1)
                    Console.WriteLine($"\n\nAcertou num Navio!!\n");
                else
                    Console.WriteLine($"\n\nAcertou em {acertou} Navios!!\n");

                // Apresentação da tabela dos navios do computador nos quais o utilizador acertou
                Console.WriteLine("Navios Afundados:\n");
                Console.WriteLine($"     0   1   2   3   4");
                Console.WriteLine($"   ┌───┬───┬───┬───┬───┐");
                Console.WriteLine($" 0 │ {naviosAfundados[0, 0]} │ {naviosAfundados[0, 1]} │ {naviosAfundados[0, 2]} │ {naviosAfundados[0, 3]} │ {naviosAfundados[0, 4]} │");
                Console.WriteLine($"   ├───┼───┼───┼───┼───┤");
                Console.WriteLine($" 1 │ {naviosAfundados[1, 0]} │ {naviosAfundados[1, 1]} │ {naviosAfundados[1, 2]} │ {naviosAfundados[1, 3]} │ {naviosAfundados[1, 4]} │");
                Console.WriteLine($"   ├───┼───┼───┼───┼───┤");
                Console.WriteLine($" 2 │ {naviosAfundados[2, 0]} │ {naviosAfundados[2, 1]} │ {naviosAfundados[2, 2]} │ {naviosAfundados[2, 3]} │ {naviosAfundados[2, 4]} │");
                Console.WriteLine($"   ├───┼───┼───┼───┼───┤");
                Console.WriteLine($" 3 │ {naviosAfundados[3, 0]} │ {naviosAfundados[3, 1]} │ {naviosAfundados[3, 2]} │ {naviosAfundados[3, 3]} │ {naviosAfundados[3, 4]} │");
                Console.WriteLine($"   ├───┼───┼───┼───┼───┤");
                Console.WriteLine($" 4 │ {naviosAfundados[4, 0]} │ {naviosAfundados[4, 1]} │ {naviosAfundados[4, 2]} │ {naviosAfundados[4, 3]} │ {naviosAfundados[4, 4]} │");
                Console.WriteLine($"   └───┴───┴───┴───┴───┘");
            }
            // Mensagem caso o utilizador não acerte em nenhum navio
            else
                Console.Write("\nNão acertou em nenhum navio :(\n");
        }
    }
}
