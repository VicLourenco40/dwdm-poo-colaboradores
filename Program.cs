namespace DwdmPooColaboradores {
    public class Colaborador {
        // Definição de constantes.
        public const double SubsidioAlimentacao = 140;

        // Definição de propriedades.
        private int codigo;
        private string nome;
        private double vencimento;
        private double plafondAlimentacao;
        private bool seguroSaude;

        // Definição de métodos construtores.

        // Permite a atribuição direta de um valor ao
        // plafond de alimentação.
        public Colaborador(int codigo, string nome, double vencimento,
                           double plafondAlimentacao, bool seguroSaude) {
            this.codigo = codigo;
            this.nome = nome;
            this.vencimento = vencimento;
            this.seguroSaude = seguroSaude;
            this.plafondAlimentacao = plafondAlimentacao;
        }

        // Permite a atribuição direta do valor mensal do
        // subsídio de alimentação ao plafond de alimentação.
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

        // Definição de getters.
        public string GetNome() { return nome; }

        public double GetVencimento() { return vencimento; }

        public double GetPlafondAlimentacao() { return plafondAlimentacao; }

        public bool GetSeguroSaude() { return seguroSaude; }

        // Retornar uma string em formato CSV com todas as propriedades do colaborador,
        // preparada para exportação.
        public string GetCSVString() {
            return $"{this.codigo}, {this.nome}, {this.vencimento}, {this.plafondAlimentacao}, {this.seguroSaude}";
        }

        // Definição de Métodos.

        // Mostrar as propriedades da instância do Colaborador.
        public void ListarColaborador() {
            Console.Write($"Código: {this.codigo}\n" +
                          $"Nome: {this.nome}\n" +
                          $"Vencimento: {this.vencimento}€\n" +
                          $"Plafond de alimentação: {this.plafondAlimentacao}€\n" +
                          $"Seguro de saúde: {this.seguroSaude}\n");
        }

        // Alterar as propriedades da instância do Colaborador.
        public void AlterarColaborador(double vencimento, double plafondAlimentacao, bool seguroSaude) {
            this.vencimento = vencimento;
            this.plafondAlimentacao += plafondAlimentacao;
            this.seguroSaude = seguroSaude;
        }

        // Carregar o cartão refeição da instância do Colaborador.
        public void CarregarCartaoRefeicao(double valor) {
            this.plafondAlimentacao += valor;
        }

        // Usar o cartão refeição da instância do Colaborador.
        // Retorna -1 (erro) ou 0 (sucesso) para permitir o print de uma mensagem no método chamador.
        public int UsarCartaoRefeicao(double valor) {
            if (this.plafondAlimentacao < valor) {
                return -1;
            }

            this.plafondAlimentacao -= valor;

            return 0;
        }
    }

    internal class Program {
        // Caminho para o ficheiro CSV.
        private const string FilePath = "colaboradores.csv";

        private static Colaborador[] colaboradores = [];

        // Transformar uma opção S/n no seu respetivo valor booleano.
        // O "S" em "S/n" é capitalizado, pois é a opção escolhida por defeito caso o utilizador
        // se recuse a enviar uma resposta.
        public static bool GetBool(string boolString) {
            boolString = boolString.ToLower();

            if (boolString == "" || boolString == "s") {
                return true;
            }

            return false;
        }

        // Inserir X novos colaboradores na array.
        public static void InserirColaboradores() {
            Console.Write("Nº de colaboradores a inserir: ");
            int numero = int.Parse(Console.ReadLine());

            // Índice inicial para a inserção de colaboradores.
            int oldLength = colaboradores.Length;

            // Aumentar o tamanho da array mediante a quantidade pedida.
            Array.Resize(ref colaboradores, colaboradores.Length + numero);

            for (int i = 0; i < numero; i++) {
                Console.Write($"\nColaborador {i + 1}/{numero}\n");

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

                // Instanciar um novo colaborador com as propriedades obtidas acima.
                Colaborador colaborador = new(codigo, nome, vencimento,
                                              subsidioAlimentacao, seguroSaude);
                colaboradores[oldLength + i] = colaborador;
            }

            Console.Write("\nColaboradores inseridos.\n\n");
        }

        // Listar todos os colaboradores na presentes na array.
        public static void ListarColaboradores() {
            for (int i = 0; i < colaboradores.Length; i++) {
                // Contador visual.
                Console.Write($"Colaborador {i + 1}/{colaboradores.Length}\n");

                colaboradores[i].ListarColaborador();

                Console.Write("\n");
            }
        }

        // Retornar o índice do colaborador na array pelo nome.
        public static int FindColaboradorByNome() {
            Console.Write("Nome a pesquisar: ");
            string nome = Console.ReadLine().ToLower();

            // Percorrer todos os colaboradores até encontrar um com o nome equivalente ao pedido.
            for (int i = 0; i < colaboradores.Length; i++) {
                if (nome == colaboradores[i].GetNome().ToLower()) {
                    return i;
                }
            }

            // Caso nenhum colaborador seja encontrado, retorna -1 (erro).
            Console.Write("\nNenhum colaborador encontrado.\n");
            return -1;
        }

        // Mostrar os dados de um colaborador.
        public static void ConsultarColaborador() {
            int index = FindColaboradorByNome();

            Console.Write("\n");

            // Retornar caso nenhum colaborador tenha sido encontrado.
            if (index == -1) { return; }

            colaboradores[index].ListarColaborador();

            Console.Write("\n");
        }

        // Alterar os dados de um colaborador.
        public static void AlterarColaborador() {
            int index = FindColaboradorByNome();

            Console.Write("\n");

            // Retornar caso nenhum colaborador tenha sido encontrado.
            if (index == -1) { return; }

            Console.Write("Vencimento: ");
            double vencimento = double.Parse(Console.ReadLine());

            Console.Write("Plafond de alimentação a adicionar: ");
            double plafondAlimentacao = double.Parse(Console.ReadLine());

            Console.Write("Seguro de saúde (S/n): ");
            bool seguroSaude = GetBool(Console.ReadLine());

            colaboradores[index].AlterarColaborador(vencimento, plafondAlimentacao, seguroSaude);

            Console.Write("\nColaborador alterado com sucesso.\n\n");
        }

        // Eliminar um colaborador da array.
        public static void EliminarColaborador() {
            int index = FindColaboradorByNome();

            Console.Write("\n");

            // Retornar caso nenhum colaborador tenha sido encontrado.
            if (index == -1) { return; }

            // Reajustar os colaboradores acima do eliminado na array.
            for (int i = index; i < colaboradores.Length - 1; i++) {
                colaboradores[i] = colaboradores[i + 1];
            }

            Array.Resize(ref colaboradores, colaboradores.Length - 1);

            Console.Write("Colaborador eliminado com sucesso.\n\n");
        }

        // Consultar o saldo existente do cartão de refeição de um colaborador.
        public static void ConsultarSaldoCartao() {
            int index = FindColaboradorByNome();

            Console.Write("\n");

            // Retornar caso nenhum colaborador tenha sido encontrado.
            if (index == -1) { return; }

            double saldo = colaboradores[index].GetPlafondAlimentacao();

            Console.Write($"Saldo do cartão de alimentação: {saldo}€\n\n");
        }

        // Usar o cartão de refeição, caso o valor introduzido não exceda o saldo existente.
        public static void UsarCartaoRefeicao() {
            int index = FindColaboradorByNome();

            Console.Write("\n");

            // Retornar caso nenhum colaborador tenha sido encontrado.
            if (index == -1) { return; }

            double saldo = colaboradores[index].GetPlafondAlimentacao();

            Console.Write($"Saldo do cartão de alimentação: {saldo}€\n\n");

            Console.Write("Valor a descontar: ");
            double valor = double.Parse(Console.ReadLine());

            int resultado = colaboradores[index].UsarCartaoRefeicao(valor);

            // Retornar caso o valor introduzido exceda o saldo existente.
            if (resultado == -1) {
                Console.Write("\nSaldo insuficiente.\n\n");
                return;
            }

            saldo = colaboradores[index].GetPlafondAlimentacao();

            Console.Write("\nValor descontado com sucesso.\n\n" +
                         $"Saldo do cartão de alimentação: {saldo}€\n\n");
        }

        // Carregar o cartão de refeição de um colaborador em particular.
        public static void CarregarCartaoRefeicao() {
            int index = FindColaboradorByNome();

            Console.Write("\n");

            // Retornar caso nenhum colaborador tenha sido encontrado.
            if (index == -1) { return; }

            double saldo = colaboradores[index].GetPlafondAlimentacao();

            Console.Write($"Saldo do cartão de alimentação: {saldo}€\n\n");

            Console.Write("Valor a carregar: ");
            double valor = double.Parse(Console.ReadLine());

            colaboradores[index].CarregarCartaoRefeicao(valor);

            saldo = colaboradores[index].GetPlafondAlimentacao();

            Console.Write("\nValor descontado com sucesso.\n\n" +
                         $"Saldo do cartão de alimentação: {saldo}€\n\n");
        }

        // Carregar o cartão de refeição de todos os colaboradores
        // com o valor do subsídio de alimentação (140€).
        public static void CarregarCartaoRefeicaoTodos() {
            // Constante pertencente à classe Colaborador que define qual o valor mensal do
            // subsídio de alimentação (evitando assim "números mágicos").
            double valor = Colaborador.SubsidioAlimentacao;

            for (int i = 0; i < colaboradores.Length; i++) {
                colaboradores[i].CarregarCartaoRefeicao(valor);
            }

            Console.Write($"Cartões carregados com {valor}€.\n\n");
        }

        // Mostrar a média de vencimentos dos colaboradores.
        public static void MediaVencimentos() {
            double soma = 0;

            // Somar os vencimentos de todos os colaboradores.
            for (int i = 0; i < colaboradores.Length; i++) {
                soma += colaboradores[i].GetVencimento();
            }

            // Dividir a soma anterior pelo número de colaboradores.
            double media = Math.Round(soma / colaboradores.Length, 2);

            Console.Write($"A média dos vencimentos é de {media}€.\n\n");
        }

        // Mostrar o colaborador com o maior vencimento, e o seu respetivo valor.
        public static void MaiorVencimento() {
            string nome = colaboradores[0].GetNome();
            double vencimento = colaboradores[0].GetVencimento();

            // Percorrer todos os colaboradores, e obter o seu respetivo nome e vencimento
            // caso este seja maior do que o que temos guardado.
            for (int i = 1; i < colaboradores.Length; i++) {
                if (vencimento < colaboradores[i].GetVencimento()) {
                    nome = colaboradores[i].GetNome();
                    vencimento = colaboradores[i].GetVencimento();
                }
            }

            Console.Write($"O colaborador com maior vencimento é {nome}, com o valor de {vencimento}€.\n\n");
        }

        // Mostrar o colaborador com o menor vencimento, e o seu respetivo valor.
        public static void MenorVencimento() {
            double menor = colaboradores[0].GetVencimento();
            string nome = colaboradores[0].GetNome();

            // Percorrer todos os colaboradores, e obter o seu respetivo nome e vencimento caso este seja
            // menor do que o que temos guardado.
            for (int i = 1; i < colaboradores.Length; i++) {
                if (colaboradores[i].GetVencimento() < menor) {
                    menor = colaboradores[i].GetVencimento();
                    nome = colaboradores[i].GetNome();
                }
            }

            Console.Write($"O colaborador com menor vencimento é {nome}, com o valor de {menor}€.\n\n");
        }

        // Mostrar a lista de colaboradores com seguro de saúde.
        // Esta solução complexa é necessária para que possamos mostrar o contador visual devidamente.
        public static void ComSeguroSaude() {
            int[] indices = [];

            // Obter os índices dos colaboradores com seguro de saúde.
            for (int i = 0; i < colaboradores.Length; i++) {
                if (colaboradores[i].GetSeguroSaude()) {
                    Array.Resize(ref indices, indices.Length + 1);
                    indices[indices.Length - 1] = i;
                }
            }

            for (int i = 0 ; i < indices.Length; i++) {
                // Contador visual.
                Console.Write($"Colaborador {i + 1}/{indices.Length}\n");
                colaboradores[indices[i]].ListarColaborador();
                Console.Write("\n");
            }
        }

        // Menu principal do programa.
        // Entre operações, a consola é limpa.
        public static void Menu() {
            Console.Clear();
            Console.Write("Gestão de colaboradores\n\n" +
                          " 1. Inserir colaboradores\n" +
                          " 2. Listar colaboradores\n" +
                          " 3. Consultar colaborador\n" +
                          " 4. Alterar colaborador\n" +
                          " 5. Eliminar colaborador\n" +
                          " 6. Consultar saldo do cartão de alimentação\n" +
                          " 7. Usar cartão de alimentação\n" +
                          " 8. Carregar cartão de alimentação de colaborador\n" +
                          " 9. Carregar cartão de alimentação de todos os colaboradores\n" +
                          "10. Calcular média dos vencimentos\n" +
                          "11. Colaborador com maior vencimento\n" +
                          "12. Colaborador com menor vencimento\n" +
                          "13. Listar colaboradores com seguro de saúde\n" +
                          " 0. Sair\n\n" +
                          ": ");

            // Tipo string para evitar exceções de tipo de dado, caso o utilizador envie
            // caracteres não numéricos.
            string opcao = Console.ReadLine();

            Console.Clear();

            // Compartmentalizar o código em métodos organiza o programa e facilita a sua leitura.
            switch (opcao) {
                case "1":
                    InserirColaboradores();
                    break;

                case "2":
                    ListarColaboradores();
                    break;

                case "3":
                    ConsultarColaborador();
                    break;
                
                case "4":
                    AlterarColaborador();
                    break;

                case "5":
                    EliminarColaborador();
                    break;

                case "6":
                    ConsultarSaldoCartao();
                    break;

                case "7":
                    UsarCartaoRefeicao();
                    break;
                    
                case "8":
                    CarregarCartaoRefeicao();
                    break;

                case "9":
                    CarregarCartaoRefeicaoTodos();
                    break;

                case "10":
                    MediaVencimentos();
                    break;

                case "11":
                    MaiorVencimento();
                    break;

                case "12":
                    MenorVencimento();
                    break;

                case "13":
                    ComSeguroSaude();
                    break;

                case "0":
                    // Terminar a execução do programa sem erros (código 0).
                    Environment.Exit(0);
                    break;

                default:
                    Console.Write("Opção Inválida.\n\n");
                    break;
            }

            // Ao fim de cada operação, esta mensagem dá tempo ao utilizador para visualizar
            // o resultado antes de prosseguir.
            Console.Write("Prima ENTER para continuar...");
            Console.ReadLine();
        }

        // Exporta um ficheiro em formato CSV, que contém todos os registos de colaboradores.
        public static void WriteCSV() {
            using (StreamWriter sw = new(FilePath)) {
                // Escrever o cabeçalho.
                sw.WriteLine("codigo, nome, vencimento, plafondAlimentacao, seguroSaude");

                // Escrever linha a linha a informação de cada colaborador presente na array.
                for (int i = 0; i < colaboradores.Length; i++) {
                    sw.WriteLine(colaboradores[i].GetCSVString());
                }
            }
        }

        // Importa um ficheiro em formato CSV, que inclua registos de colaboradores.
        public static void ReadCSV() {
            // Ler o ficheiro, passando à frente o cabeçalho.
            string[] linhas = File.ReadAllLines(FilePath).Skip(1).ToArray();

            // Aumentar a array mediante a quantidade de colaboradores a adicionar.
            Array.Resize(ref colaboradores, linhas.Length);

            for (int i = 0; i < linhas.Length; i++) {
                // Separar cada string CSV em propriedades individuais.
                // O delimitador escolhido para o CSV é ", ".
                string[] propriedades = linhas[i].Split(", ");

                // Instanciar um novo colaborador com as propriedades obtidas acima,
                // convertendo-as no seu respetivo tipo de dado.
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
            // A mudança do encoding é necessário para a consola mostrar acentuação devidamente,
            // entre outros caracteres especiais.
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Importar os colaboradores existentes no ficheiro CSV.
            // No caso deste ficheiro não existir, criar um ficheiro vazio.
            if (File.Exists(FilePath)) {
                ReadCSV();
            }
            else {
                WriteCSV();
            }

            // Loop principal do programa.
            // Ao fim de cada operação, exportar a atualização para o ficheiro.
            while (true) {
                Menu();
                WriteCSV();
            }
        }
    }
}
