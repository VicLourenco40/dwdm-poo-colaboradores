namespace DwdmPooColaboradores {
    public class Colaborador {
        public const double SubsidioAlimentacao = 140;

        private int codigo;
        private string nome;
        private double vencimento;
        private double plafondAlimentacao;
        private bool seguroSaude;

        public Colaborador(int codigo, string nome, double vencimento,
                           double plafondAlimentacao, bool seguroSaude) {
            this.codigo = codigo;
            this.nome = nome;
            this.vencimento = vencimento;
            this.seguroSaude = seguroSaude;
            this.plafondAlimentacao = plafondAlimentacao;
        }

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

        public string GetNome() { return nome; }

        public double GetVencimento() { return vencimento; }

        public double GetPlafondAlimentacao() { return plafondAlimentacao; }

        public string GetCSVString() {
            return $"{this.codigo}, {this.nome}, {this.vencimento}, {this.plafondAlimentacao}, {this.seguroSaude}";
        }

        public void ListarColaborador() {
            Console.Write($"Código: {this.codigo}\n" +
                          $"Nome: {this.nome}\n" +
                          $"Vencimento: {this.vencimento}€\n" +
                          $"Plafond de alimentação: {this.plafondAlimentacao}€\n" +
                          $"Seguro de saúde: {this.seguroSaude}\n");
        }

        public void AlterarColaborador(double vencimento, double plafondAlimentacao, bool seguroSaude) {
            this.vencimento = vencimento;
            this.plafondAlimentacao += plafondAlimentacao;
            this.seguroSaude = seguroSaude;
        }

        public void CarregarCartaoRefeicao(double valor) {
            this.plafondAlimentacao += valor;
        }

        public int UsarCartaoRefeicao(double valor) {
            if (this.plafondAlimentacao < valor) {
                return -1;
            }

            this.plafondAlimentacao -= valor;

            return 0;
        }
    }

    internal class Program {
        private const string FilePath = ".\\colaboradores.csv";

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
        public static void ListarColaboradores() {
            for (int i = 0; i < colaboradores.Length; i++) {
                Console.Write($"Colaborador {i + 1}\n");

                colaboradores[i].ListarColaborador();

                Console.Write("\n");
            }
        }

        public static int FindColaboradorByNome() {
            Console.Write("Nome a pesquisar: ");
            string nome = Console.ReadLine().ToLower();

            for (int i = 0; i < colaboradores.Length; i++) {
                if (nome == colaboradores[i].GetNome().ToLower()) {
                    return i;
                }
            }

            Console.Write("\nNenhum colaborador encontrado.\n");

            return -1;
        }

        public static void ConsultarColaborador() {
            int index = FindColaboradorByNome();

            Console.Write("\n");

            if (index == -1) { return; }

            colaboradores[index].ListarColaborador();

            Console.Write("\n");
        }

        public static void AlterarColaborador() {
            int index = FindColaboradorByNome();

            Console.Write("\n");

            if (index == -1) { return; }

            Console.Write("Vencimento: ");
            double vencimento = double.Parse(Console.ReadLine());

            Console.Write("Plafond de alimentação a adicionar: ");
            double plafondAlimentacao = double.Parse(Console.ReadLine());

            Console.Write("Seguro de saúde (S/n): ");
            bool seguroSaude = GetBool(Console.ReadLine());

            colaboradores[index].AlterarColaborador(vencimento, plafondAlimentacao, seguroSaude);

            Console.Write("\nRegisto alterado com sucesso.\n\n");
        }

        public static void EliminarColaborador() {
            int index = FindColaboradorByNome();

            Console.Write("\n");

            if (index == -1) { return; }

            for (int i = index; i < colaboradores.Length - 1; i++) {
                colaboradores[i] = colaboradores[i + 1];
            }

            Array.Resize(ref colaboradores, colaboradores.Length - 1);

            Console.Write("Colaborador eliminado com sucesso.\n\n");
        }

        public static void ConsultarSaldoCartao() {
            int index = FindColaboradorByNome();

            Console.Write("\n");

            if (index == -1) { return; }

            double saldo = colaboradores[index].GetPlafondAlimentacao();

            Console.Write($"Saldo do subsídio de alimentação: {saldo}€\n\n");
        }
        public static void UsarCartaoRefeicao() {
            int index = FindColaboradorByNome();

            Console.Write("\n");

            if (index == -1) { return; }

            Console.Write("Valor a descontar: ");
            double valor = double.Parse(Console.ReadLine());

            int result = colaboradores[index].UsarCartaoRefeicao(valor);

            if (result == -1) {
                Console.Write("\nSaldo insuficiente.\n\n");
                return;
            }

            Console.Write("\nValor descontado com sucesso.\n\n");
        }

        public static void CarregarCartaoRefeicao() {
            int index = FindColaboradorByNome();

            Console.Write("\n");

            if (index == -1) { return; }

            Console.Write($"O saldo atual do cartão de {colaboradores[index].GetNome()} é de {colaboradores[index].GetPlafondAlimentacao()}€.\n");

            Console.Write("\nValor a carregar: ");
            double valor = double.Parse(Console.ReadLine());

            colaboradores[index].CarregarCartaoRefeicao(valor);

            Console.Write($"\nCartão carregado com {valor}€.\n");
            Console.Write($"\nO saldo atual do cartão é de {colaboradores[index].GetPlafondAlimentacao()}€.\n\n");
        }

        public static void CarregarCartaoRefeicaoTodos() {
            double valor = Colaborador.SubsidioAlimentacao;

            for (int i = 0; i < colaboradores.Length; i++) {
                colaboradores[i].CarregarCartaoRefeicao(valor);
            }

            Console.Write($"Cartões de Refeição carregados ({valor}€).\n\n");
        }

        public static void MediaVencimentos() {
            double soma = 0;

            for (int i = 0; i < colaboradores.Length; i++) {
                soma += colaboradores[i].GetVencimento();
            }

            double media = Math.Round(soma / colaboradores.Length, 2);

            Console.Write($"A média dos vencimentos é de {media}€.\n\n");
        }

        public static void MaiorVencimento() {
            string nome = colaboradores[0].GetNome();
            double vencimento = colaboradores[0].GetVencimento();

            for (int i = 1; i < colaboradores.Length; i++) {
                if (vencimento < colaboradores[i].GetVencimento()) {
                    nome = colaboradores[i].GetNome();
                    vencimento = colaboradores[i].GetVencimento();
                }
            }

            Console.Write($"O colaborador com maior vencimento é {nome}, com o valor de {vencimento}€.\n\n");
        }

        public static void MenorVencimento() {
            double menor = colaboradores[0].GetVencimento();
            string nome = colaboradores[0].GetNome();

            for (int i = 1; i < colaboradores.Length; i++) {
                if (colaboradores[i].GetVencimento() < menor) {
                    menor = colaboradores[i].GetVencimento();
                    nome = colaboradores[i].GetNome();
                }
            }

            Console.Write($"O colaborador com menor vencimento é {nome}, com o valor de {menor}€.\n\n");
        }

        public static void Menu() {
            Console.Clear();
            Console.Write("Gestão de colaboradores\n\n" +
                          " 1. Inserir colaboradores\n" +
                          " 2. Listar colaboradores\n" +
                          " 3. Consultar colaborador\n" +
                          " 4. Alterar colaborador\n" +
                          " 5. Eliminar colaborador\n" +
                          " 6. Consultar saldo de cartão de colaborador\n" +
                          " 7. Usar cartão para refeições\n" +
                          " 8. Carregar cartão de alimentação de colaborador\n" +
                          " 9. Carregar cartão de alimentação de todos os colaboradores\n" +
                          "10. Calcular média dos vencimentos\n" +
                          "11. Colaborador com maior vencimento\n" +
                          "12. Colaborador com menor vencimento\n" +
                          "13. Listar colaboradores com seguro de saúde\n" +
                          " 0. Sair\n\n" +
                          ": ");

            int opcao = int.Parse(Console.ReadLine());

            Console.Clear();
            switch (opcao) {
                case 1:
                    InserirColaboradores();
                    break;

                case 2:
                    ListarColaboradores();
                    break;

                case 3:
                    ConsultarColaborador();
                    break;
                
                case 4:
                    AlterarColaborador();
                    break;

                case 5:
                    EliminarColaborador();
                    break;

                case 6:
                    ConsultarSaldoCartao();
                    break;

                case 7:
                    UsarCartaoRefeicao();
                    break;
                    
                case 8:
                    CarregarCartaoRefeicao();
                    break;

                case 9:
                    CarregarCartaoRefeicaoTodos();
                    break;

                case 10:
                    MediaVencimentos();
                    break;

                case 11:
                    MaiorVencimento();
                    break;

                case 12:
                    MenorVencimento();
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

        public static void WriteCSV() {
            using (StreamWriter sw = new(FilePath)) {
                sw.WriteLine("codigo, nome, vencimento, plafondAlimentacao, seguroSaude");

                for (int i = 0; i < colaboradores.Length; i++) {
                    sw.WriteLine(colaboradores[i].GetCSVString());
                }
            }
        }

        public static void ReadCSV() {
            string[] linhas = File.ReadAllLines(FilePath).Skip(1).ToArray();

            Array.Resize(ref colaboradores, linhas.Length);

            for (int i = 0; i < linhas.Length; i++) {
                string[] propriedades = linhas[i].Split(", ");

                Colaborador colaborador = new(
                    int.Parse(propriedades[0]),
                    propriedades[1],
                    double.Parse(propriedades[2]),
                    double.Parse(propriedades[3]),
                    bool.Parse(propriedades[4])
                );

                colaboradores[i] = colaborador;
            }
        }

        static void Main(string[] args) {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            if (!File.Exists(FilePath)) {
                WriteCSV();
            }

            ReadCSV();

            while (true) {
                Menu();
                WriteCSV();
            }
        }
    }
}
