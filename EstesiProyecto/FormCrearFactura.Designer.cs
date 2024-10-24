namespace EstesiProyecto
{
    partial class FormCrearFactura
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtFacturado = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtNetoCancelar = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtValorNC = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbMotivo = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtNNotaCredito = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dtpSugerido = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpVencimiento = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpFacturacion = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNRetencion = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNFactura = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtResultProveedor = new System.Windows.Forms.TextBox();
            this.txtRucProveedor = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbSucursal = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnGuardar
            // 
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Location = new System.Drawing.Point(635, 594);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(106, 40);
            this.btnGuardar.TabIndex = 57;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtFacturado
            // 
            this.txtFacturado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFacturado.Location = new System.Drawing.Point(534, 383);
            this.txtFacturado.Name = "txtFacturado";
            this.txtFacturado.Size = new System.Drawing.Size(174, 26);
            this.txtFacturado.TabIndex = 56;
            this.txtFacturado.TextChanged += new System.EventHandler(this.txtFacturado_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(572, 345);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(105, 20);
            this.label13.TabIndex = 55;
            this.label13.Text = "Valor Factura";
            // 
            // txtNetoCancelar
            // 
            this.txtNetoCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNetoCancelar.HideSelection = false;
            this.txtNetoCancelar.Location = new System.Drawing.Point(649, 505);
            this.txtNetoCancelar.Name = "txtNetoCancelar";
            this.txtNetoCancelar.Size = new System.Drawing.Size(174, 26);
            this.txtNetoCancelar.TabIndex = 53;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(676, 446);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(110, 20);
            this.label12.TabIndex = 52;
            this.label12.Text = "Neto Cancelar";
            // 
            // txtValorNC
            // 
            this.txtValorNC.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValorNC.Location = new System.Drawing.Point(754, 382);
            this.txtValorNC.Name = "txtValorNC";
            this.txtValorNC.Size = new System.Drawing.Size(174, 26);
            this.txtValorNC.TabIndex = 51;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(805, 345);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 20);
            this.label11.TabIndex = 50;
            this.label11.Text = "Valor N/C";
            // 
            // cmbMotivo
            // 
            this.cmbMotivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMotivo.FormattingEnabled = true;
            this.cmbMotivo.Location = new System.Drawing.Point(747, 260);
            this.cmbMotivo.Name = "cmbMotivo";
            this.cmbMotivo.Size = new System.Drawing.Size(165, 28);
            this.cmbMotivo.TabIndex = 49;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(805, 222);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 20);
            this.label10.TabIndex = 48;
            this.label10.Text = "Motivo";
            // 
            // txtNNotaCredito
            // 
            this.txtNNotaCredito.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNNotaCredito.Location = new System.Drawing.Point(545, 259);
            this.txtNNotaCredito.Name = "txtNNotaCredito";
            this.txtNNotaCredito.Size = new System.Drawing.Size(174, 26);
            this.txtNNotaCredito.TabIndex = 47;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(572, 222);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(119, 20);
            this.label9.TabIndex = 46;
            this.label9.Text = "Nº Nota Credito";
            // 
            // dtpSugerido
            // 
            this.dtpSugerido.CalendarFont = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpSugerido.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpSugerido.Location = new System.Drawing.Point(113, 594);
            this.dtpSugerido.Name = "dtpSugerido";
            this.dtpSugerido.Size = new System.Drawing.Size(330, 26);
            this.dtpSugerido.TabIndex = 45;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(188, 556);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 20);
            this.label8.TabIndex = 44;
            this.label8.Text = "Fecha Sugerida";
            // 
            // dtpVencimiento
            // 
            this.dtpVencimiento.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpVencimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpVencimiento.Location = new System.Drawing.Point(113, 484);
            this.dtpVencimiento.Name = "dtpVencimiento";
            this.dtpVencimiento.Size = new System.Drawing.Size(330, 26);
            this.dtpVencimiento.TabIndex = 43;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(184, 446);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(146, 20);
            this.label7.TabIndex = 42;
            this.label7.Text = "Fecha Vencimiento";
            // 
            // dtpFacturacion
            // 
            this.dtpFacturacion.CalendarFont = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFacturacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFacturacion.Location = new System.Drawing.Point(113, 384);
            this.dtpFacturacion.Name = "dtpFacturacion";
            this.dtpFacturacion.Size = new System.Drawing.Size(330, 26);
            this.dtpFacturacion.TabIndex = 41;
            this.dtpFacturacion.Value = new System.DateTime(2024, 10, 23, 0, 40, 55, 0);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(188, 345);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(142, 20);
            this.label6.TabIndex = 40;
            this.label6.Text = "Fecha Facturacion";
            // 
            // txtNRetencion
            // 
            this.txtNRetencion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNRetencion.Location = new System.Drawing.Point(335, 260);
            this.txtNRetencion.Name = "txtNRetencion";
            this.txtNRetencion.Size = new System.Drawing.Size(174, 26);
            this.txtNRetencion.TabIndex = 39;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(361, 222);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 20);
            this.label5.TabIndex = 38;
            this.label5.Text = "Nº Retencion";
            // 
            // txtNFactura
            // 
            this.txtNFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNFactura.Location = new System.Drawing.Point(131, 260);
            this.txtNFactura.Name = "txtNFactura";
            this.txtNFactura.Size = new System.Drawing.Size(174, 26);
            this.txtNFactura.TabIndex = 37;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(169, 222);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 20);
            this.label4.TabIndex = 36;
            this.label4.Text = "Nº Factura";
            // 
            // txtResultProveedor
            // 
            this.txtResultProveedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResultProveedor.Location = new System.Drawing.Point(635, 154);
            this.txtResultProveedor.Name = "txtResultProveedor";
            this.txtResultProveedor.ReadOnly = true;
            this.txtResultProveedor.Size = new System.Drawing.Size(262, 29);
            this.txtResultProveedor.TabIndex = 35;
            this.txtResultProveedor.TabStop = false;
            this.txtResultProveedor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtRucProveedor
            // 
            this.txtRucProveedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRucProveedor.Location = new System.Drawing.Point(378, 156);
            this.txtRucProveedor.Name = "txtRucProveedor";
            this.txtRucProveedor.Size = new System.Drawing.Size(174, 26);
            this.txtRucProveedor.TabIndex = 34;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(375, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 24);
            this.label3.TabIndex = 33;
            this.label3.Text = "Proveedor";
            // 
            // cmbSucursal
            // 
            this.cmbSucursal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSucursal.FormattingEnabled = true;
            this.cmbSucursal.Location = new System.Drawing.Point(146, 155);
            this.cmbSucursal.Name = "cmbSucursal";
            this.cmbSucursal.Size = new System.Drawing.Size(136, 28);
            this.cmbSucursal.TabIndex = 32;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(142, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 24);
            this.label2.TabIndex = 31;
            this.label2.Text = "Sucursal";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(392, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 33);
            this.label1.TabIndex = 30;
            this.label1.Text = "Crear Factura";
            // 
            // FormCrearFactura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 683);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.txtFacturado);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtNetoCancelar);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtValorNC);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cmbMotivo);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtNNotaCredito);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.dtpSugerido);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dtpVencimiento);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dtpFacturacion);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtNRetencion);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtNFactura);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtResultProveedor);
            this.Controls.Add(this.txtRucProveedor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbSucursal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormCrearFactura";
            this.Text = "FormCrearFactura";
            this.Load += new System.EventHandler(this.FormCrearFactura_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TextBox txtFacturado;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtNetoCancelar;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtValorNC;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbMotivo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtNNotaCredito;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtpSugerido;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpVencimiento;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpFacturacion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNRetencion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNFactura;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtResultProveedor;
        private System.Windows.Forms.TextBox txtRucProveedor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbSucursal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}