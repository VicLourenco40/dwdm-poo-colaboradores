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
                          "1. Inserir colaborador\n" +
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
