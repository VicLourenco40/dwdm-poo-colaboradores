namespace DwdmPooColaboradores {
    public class Colaborador {
        public const double SubsidioAlimentacao = 140;

        int codigo;
        string nome;
        double vencimento;
        double plafondAlimentacao;
        bool seguroSaude;

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
        static void Main(string[] args) {
            Colaborador[] colaboradores = [];
        }
    }
}
