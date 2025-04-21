namespace LoanManagementSys;

public partial class MainForm : Form
{
    private LoanSystem loanSystem;

    public MainForm()
    {
        InitializeComponent();
        loanSystem = new LoanSystem(lstItems, lstOutput);
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        loanSystem.Start();

    }

    private void btnStop_Click(object sender, EventArgs e)
    {
        loanSystem.Stop();
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        loanSystem.Stop();
        Application.Exit();
    }

}