namespace DwdmPooColaboradores {
    public class Colaborador {
        public const double SubsidioAlimentacao = 140;

        private int codigo;
        private string nome;
        private double vencimento;
        private double plafondAlimentacao;
        private bool seguroSaude;

        public Colaborador(int codigo, string nome, double vencimento,
                           bool plafondAlimentacao, bool seguroSaude) {
            this.codigo = codigo;
            this.nome = nome;
            this.vencimento = vencimento;
            this.seguroSaude = seguroSaude;
            if (plafondAlimentacao) {
                this.plafondAlimentacao = SubsidioAlimentacao;
            }
        }
    }

    internal class Program {
        private static Colaborador[] colaboradores = [];

        public static void Exemplo() {
            Console.Write($"Número de colaboradores: {colaboradores.Length}\n\n");
        }

        public static void Menu() {
            Console.Clear();
            Console.Write("Gestão de colaboradores\n\n" +
                          "1. Exemplo\n" +
                          "0. Sair\n\n" +
                          ": ");

            int opcao = int.Parse(Console.ReadLine());

            Console.Clear();

            switch (opcao) {
                case 1:
                    Exemplo();
                    break;

                case 0:
                    Environment.Exit(0);
                    break;

                default:
                    Console.Write("Opção Inválida.\n\n");
                    break;
            }

            Console.Write("Prima ENTER para continuar...");
            Console.ReadLine();
        }

        static void Main(string[] args) {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true) {
                Menu();
            }
        }
    }
}
