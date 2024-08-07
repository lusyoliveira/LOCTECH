Imports System.Data.SqlClient
Public Class frmCadFuncionarios
    Dim tbFuncionarios As DataTable, ClasseFuncionario As New clsEntidades, ClasseCombo As New clsCombo
    Private Sub limpar()
        txtMatricula.Text = ""
        txtNome.Text = ""
        mskDnascimento.Text = ""
        cboEstadoCivil.Text = ""
        txtEndereco.Text = ""
        txtComplemento.Text = ""
        txtBairro.Text = ""
        txtCidade.Text = ""
        cboUf.Text = ""
        mskCep.Text = ""
        rbdFeminino.Checked = False
        rbdMasculino.Checked = False
        mskTel1.Text = ""
        mskTel2.Text = ""
        mskCel.Text = ""
        txtEmail.Text = ""
        mskRG.Text = ""
        mskCpf.Text = ""
        mskCartprof.Text = ""
        cboCargo.Text = ""
        txtSalario.Text = ""
        cboExpediente.Text = ""
        txtObs.Text = ""
        lstEntidade.Tag = 0
    End Sub

    Private Sub btnConsultar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        ClasseFuncionario.PesquisaEntidade(lstEntidade, Val(lblCodigo.Text), txtNome.Text, "FU")
    End Sub
    Private Sub btnNovo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNovo.Click
        limpar()
        rbdFeminino.Visible = True
        rbdMasculino.Visible = True
        txtMatricula.Focus()
    End Sub
    Private Sub btnSalvar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalvar.Click
        Dim MsgResult As DialogResult = MessageBox.Show("Confirma a inclusão do funcionario?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If MsgResult = DialogResult.Yes Then
            ClasseFuncionario.SalvarEntidade(txtNome.Text, txtNome.Text, "", "", txtEndereco.Text, "", txtBairro.Text, txtCidade.Text, cboUf.Text, mskCep.Text, "", mskRG.Text, mskCpf.Text, "", "FU")
            ClasseFuncionario.SalvarContato(Val(lblCodigo.Text), mskTel1.Text, mskTel2.Text, mskCel.Text, txtEmail.Text)
            ClasseFuncionario.SalvarCargo(Val(lblCodigo.Text), txtMatricula.Text, mskCartprof.Text, cboCargo.Text, txtSalario.Text, "")
            limpar()
            ClasseFuncionario.PesquisaEntidade(lstEntidade, Val(lblCodigo.Text), txtNome.Text, "FU")

        Else
            Exit Sub
        End If
    End Sub

    Private Sub btnExcluir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcluir.Click
        Dim MsgResult As DialogResult = MessageBox.Show("Confirma a Exclusão?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If MsgResult = DialogResult.Yes Then
            ClasseFuncionario.ExcluirCargo(Val(lblCodigo.Text))
            ClasseFuncionario.ExcluirContato(Val(lblCodigo.Text))
            ClasseFuncionario.ExcluirEntidade(Val(lblCodigo.Text))
            limpar()
            ClasseFuncionario.PesquisaEntidade(lstEntidade, Val(lblCodigo.Text), txtNome.Text, "FU")
        Else
            Exit Sub
        End If
    End Sub
    Private Sub AtualizaBancoDados(ByVal dt As DataTable)
        Dim MsgResult As DialogResult = MessageBox.Show("Confirma a alteração do fornecedor?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If MsgResult = DialogResult.Yes Then
            ClasseFuncionario.AlterarCargo(Val(lblCodigo.Text), ClasseFuncionario.CodCargo, txtMatricula.Text, mskCartprof.Text, cboCargo.Text, txtSalario.Text, "")
            ClasseFuncionario.AlterarContato(Val(lblCodigo.Text), ClasseFuncionario.CodContato, mskTel1.Text, mskTel2.Text, mskCel.Text, txtEmail.Text)
            ClasseFuncionario.AlterarEntidade(Val(lblCodigo.Text), txtNome.Text, txtNome.Text, "", "", txtEndereco.Text, "", txtBairro.Text, txtCidade.Text, cboUf.Text, mskCep.Text, "", mskRG.Text, mskCpf.Text, "")
            limpar()
            ClasseFuncionario.PesquisaEntidade(lstEntidade, Val(lblCodigo.Text), txtNome.Text, "FU")
        Else
            Exit Sub
        End If
    End Sub
    Private Sub frmCadFuncionarios_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ClasseFuncionario.PesquisaEntidade(lstEntidade, Val(lblCodigo.Text), txtNome.Text, "FU")
    End Sub

    Private Sub lstEntidade_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstEntidade.SelectedIndexChanged
        If lstEntidade.SelectedItems.Count > 0 Then
            lblCodigo.Text = Val(lstEntidade.SelectedItems(0).SubItems(0).Text)
            txtNome.Text = lstEntidade.SelectedItems(0).SubItems(1).Text
            txtEndereco.Text = lstEntidade.SelectedItems(0).SubItems(5).Text
            txtBairro.Text = lstEntidade.SelectedItems(0).SubItems(7).Text
            txtCidade.Text = lstEntidade.SelectedItems(0).SubItems(8).Text
            cboUf.Text = lstEntidade.SelectedItems(0).SubItems(9).Text
            mskCep.Text = lstEntidade.SelectedItems(0).SubItems(10).Text
            mskRG.Text = lstEntidade.SelectedItems(0).SubItems(12).Text
            mskCpf.Text = lstEntidade.SelectedItems(0).SubItems(13).Text
            ClasseFuncionario.ObterContato(Val(lblCodigo.Text), ClasseFuncionario)
            mskTel1.Text = ClasseFuncionario.Telefone1
            mskTel2.Text = ClasseFuncionario.Telefone2
            mskCel.Text = ClasseFuncionario.Celular
            txtEmail.Text = ClasseFuncionario.Email
            ClasseFuncionario.ObterCargo(Val(lblCodigo.Text), ClasseFuncionario)
            txtMatricula.Text = ClasseFuncionario.Matricula
            mskCartprof.Text = ClasseFuncionario.CarteiraProfissional
            cboCargo.Text = ClasseFuncionario.Cargo
            txtSalario.Text = ClasseFuncionario.Salario
        End If
    End Sub

    Private Sub cboEstadoCivil_Enter(sender As Object, e As EventArgs) Handles cboEstadoCivil.Enter
        Dim ListaCivil = ClasseCombo.PreencherComboBox("SELECT * FROM tbEstadoCivil ORDER BY Descricao", "Codigo", "Descricao")
        With Me.cboEstadoCivil
            .DataSource = ListaCivil
            .ValueMember = "Codigo"
            .DisplayMember = "Descricao"
            .SelectedIndex = "0"
        End With
    End Sub

    Private Sub txtMatricula_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            ClasseFuncionario.PesquisaEntidade(lstEntidade, Val(lblCodigo.Text), txtNome.Text, "FU")

        End If
    End Sub
End Class