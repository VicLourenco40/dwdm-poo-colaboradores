namespace DwdmPooColaboradores {
    public class Colaborador {
        public const double SubsidioAlimentacao = 140;

        private int codigo;
        private string nome;
        private double vencimento;
        private double plafondAlimentacao;
        private bool seguroSaude;

        public Colaborador(int codigo, string nome, double vencimento,
                           bool subsidioAlimentacao, bool seguroSaude) {
            this.codigo = codigo;
            this.nome = nome;
            this.vencimento = vencimento;
            this.seguroSaude = seguroSaude;
            if (subsidioAlimentacao) {
                this.plafondAlimentacao = SubsidioAlimentacao;
            }
        }

        public void ListarColaborador() {
            Console.Write($"Código: {this.codigo}\n" +
                          $"Nome: {this.nome}\n" +
                          $"Vencimento: {this.vencimento}\n" +
                          $"Plafond de alimentação: {this.plafondAlimentacao}\n" +
                          $"Seguro de saúde: {this.seguroSaude}\n");
        }
    }

    internal class Program {
        private static Colaborador[] colaboradores = [];

        public static bool GetBool(string boolString) {
            boolString = boolString.ToLower();

            if (boolString == "" || boolString == "s") {
                return true;
            }
            return false;
        }

        public static void InserirColaboradores() {
            Console.Write("Nº de colaboradores a inserir: ");
            int numero = int.Parse(Console.ReadLine());

            int oldLength = colaboradores.Length;
            Array.Resize(ref colaboradores, colaboradores.Length + numero);

            for (int i = 0; i < numero; i++) {
                Console.Write($"\nColaborador {i + 1}\n");
                
                Console.Write("Código: ");
                int codigo = int.Parse(Console.ReadLine());

                Console.Write("Nome: ");
                string nome = Console.ReadLine();

                Console.Write("Vencimento: ");
                double vencimento = double.Parse(Console.ReadLine());

                Console.Write("Subsídio de alimentação (S/n): ");
                bool subsidioAlimentacao = GetBool(Console.ReadLine());

                Console.Write("Seguro de saúde (S/n): ");
                bool seguroSaude = GetBool(Console.ReadLine());

                Colaborador colaborador = new(codigo, nome, vencimento,
                                              subsidioAlimentacao, seguroSaude);
                colaboradores[oldLength + i] = colaborador;
            }

            Console.Write("\n");
        }
        public static void listarColaboradores() {
            Console.Write("Listagem de registos de colaboradores\n");

            for (int i = 0; i < colaboradores.Length; i++) {
                Console.Write($"\nColaborador {i + 1}\n");

                colaboradores[i].ListarColaborador();
            }

            Console.Write("\n");
        }

        public static void Menu() {
            Console.Clear();
            Console.Write("Gestão de colaboradores\n\n" +
                          "1. Inserir colaboradores\n" +
                          "2. Listagem de registos de colaboradores\n" +
                          "3. Consultar o registo de um colaborador\n" +
                          "4. Alterar o registo de um colaborador\n" +
                          "5. Eliminar o registo de um colaborador\n"+
                          "6. Consultar o saldo do subsídio de alimentação de um colaborador\n"+
                          "7. Usar o cartão para as refeições\n"+
                          "8. Carregar o plafond do subsídio de alimentação de um colaborador\n"+
                          "9. Carregar o plafond do subsídio de alimentação de todos os colaboradores\n"+
                          "10. Calcular a média dos vencimentos dos colaboradores\n"+
                          "11. O nome do colaborador com o maior vencimento\n"+
                          "12. O nome do colaborador com o menor vencimento\n"+
                          "13. Listagem dos inscritos no ieguro de saúde\n"+
                          "0. Sair\n\n" +
                          ": ");

            int opcao = int.Parse(Console.ReadLine());

            Console.Clear();
            switch (opcao) {
                case 1:
                    InserirColaboradores();
                    break;

                case 2:
                    listarColaboradores();
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
