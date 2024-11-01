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
            this.dtpVencimiento = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpFacturacion = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNRetencion = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNFactura = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpRecibido = new System.Windows.Forms.DateTimePicker();
            this.label14 = new System.Windows.Forms.Label();
            this.lblTituloFac = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnGuardar
            // 
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Location = new System.Drawing.Point(477, 303);
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
            this.txtFacturado.Location = new System.Drawing.Point(510, 139);
            this.txtFacturado.Name = "txtFacturado";
            this.txtFacturado.Size = new System.Drawing.Size(174, 26);
            this.txtFacturado.TabIndex = 56;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(548, 101);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(105, 20);
            this.label13.TabIndex = 55;
            this.label13.Text = "Valor Factura";
            // 
            // dtpVencimiento
            // 
            this.dtpVencimiento.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpVencimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpVencimiento.Location = new System.Drawing.Point(380, 230);
            this.dtpVencimiento.Name = "dtpVencimiento";
            this.dtpVencimiento.Size = new System.Drawing.Size(330, 26);
            this.dtpVencimiento.TabIndex = 43;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(451, 192);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(146, 20);
            this.label7.TabIndex = 42;
            this.label7.Text = "Fecha Vencimiento";
            // 
            // dtpFacturacion
            // 
            this.dtpFacturacion.CalendarFont = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFacturacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFacturacion.Location = new System.Drawing.Point(26, 231);
            this.dtpFacturacion.Name = "dtpFacturacion";
            this.dtpFacturacion.Size = new System.Drawing.Size(330, 26);
            this.dtpFacturacion.TabIndex = 41;
            this.dtpFacturacion.Value = new System.DateTime(2024, 10, 23, 0, 40, 55, 0);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(101, 192);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(142, 20);
            this.label6.TabIndex = 40;
            this.label6.Text = "Fecha Facturacion";
            // 
            // txtNRetencion
            // 
            this.txtNRetencion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNRetencion.Location = new System.Drawing.Point(273, 139);
            this.txtNRetencion.Name = "txtNRetencion";
            this.txtNRetencion.Size = new System.Drawing.Size(174, 26);
            this.txtNRetencion.TabIndex = 39;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(299, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 20);
            this.label5.TabIndex = 38;
            this.label5.Text = "Nº Retencion";
            // 
            // txtNFactura
            // 
            this.txtNFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNFactura.Location = new System.Drawing.Point(69, 139);
            this.txtNFactura.Name = "txtNFactura";
            this.txtNFactura.Size = new System.Drawing.Size(174, 26);
            this.txtNFactura.TabIndex = 37;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(107, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 20);
            this.label4.TabIndex = 36;
            this.label4.Text = "Nº Factura";
            // 
            // dtpRecibido
            // 
            this.dtpRecibido.CalendarFont = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpRecibido.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpRecibido.Location = new System.Drawing.Point(26, 317);
            this.dtpRecibido.Name = "dtpRecibido";
            this.dtpRecibido.Size = new System.Drawing.Size(330, 26);
            this.dtpRecibido.TabIndex = 59;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(101, 279);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(120, 20);
            this.label14.TabIndex = 58;
            this.label14.Text = "Fecha Recibido";
            // 
            // lblTituloFac
            // 
            this.lblTituloFac.AutoSize = true;
            this.lblTituloFac.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTituloFac.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloFac.Location = new System.Drawing.Point(303, 38);
            this.lblTituloFac.Name = "lblTituloFac";
            this.lblTituloFac.Size = new System.Drawing.Size(197, 33);
            this.lblTituloFac.TabIndex = 60;
            this.lblTituloFac.Text = "Crear Factura";
            // 
            // FormCrearFactura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 382);
            this.Controls.Add(this.lblTituloFac);
            this.Controls.Add(this.dtpRecibido);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.txtFacturado);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.dtpVencimiento);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dtpFacturacion);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtNRetencion);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtNFactura);
            this.Controls.Add(this.label4);
            this.Name = "FormCrearFactura";
            this.Text = "FormCrearFactura";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TextBox txtFacturado;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker dtpVencimiento;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpFacturacion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNRetencion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNFactura;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpRecibido;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblTituloFac;
    }
}