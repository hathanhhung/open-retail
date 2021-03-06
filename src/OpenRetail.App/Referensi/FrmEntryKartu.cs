﻿/**
 * Copyright (C) 2017 Kamarudin (http://coding4ever.net/)
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not
 * use this file except in compliance with the License. You may obtain a copy of
 * the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations under
 * the License.
 *
 * The latest version of this file can be found at https://github.com/rudi-krsoftware/open-retail
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using OpenRetail.Model;
using OpenRetail.Bll.Api;
using OpenRetail.Helper.UI.Template;
using OpenRetail.Helper;

namespace OpenRetail.App.Referensi
{
    public partial class FrmEntryKartu : FrmEntryStandard
    {        
        private IKartuBll _bll = null; // deklarasi objek business logic layer 
        private Kartu _kartu = null;
        private bool _isNewData = false;

        public IListener Listener { private get; set; }

        public FrmEntryKartu(string header, IKartuBll bll)
            : base()
        {
            InitializeComponent();
            ColorManagerHelper.SetTheme(this, this);

            base.SetHeader(header);
            this._bll = bll;

            this._isNewData = true;
        }

        public FrmEntryKartu(string header, Kartu kartu, IKartuBll bll)
            : base()
        {
            InitializeComponent();
            ColorManagerHelper.SetTheme(this, this);

            base.SetHeader(header);
            base.SetButtonSelesaiToBatal();
            this._bll = bll;
            this._kartu = kartu;

            txtNamaKartu.Text = this._kartu.nama_kartu;

            if (this._kartu.is_debit)
                rdoKartuDebit.Checked = true;
            else
                rdoKartuKredit.Checked = true;
        }

        protected override void Simpan()
        {
            if (_isNewData)
                _kartu = new Kartu();

            _kartu.nama_kartu = txtNamaKartu.Text;
            _kartu.is_debit = rdoKartuDebit.Checked;

            var result = 0;
            var validationError = new ValidationError();

            if (_isNewData)
                result = _bll.Save(_kartu, ref validationError);
            else
                result = _bll.Update(_kartu, ref validationError);

            if (result > 0) 
            {
                Listener.Ok(this, _isNewData, _kartu);

                if (_isNewData)
                {
                    base.ResetForm(this);
                    txtNamaKartu.Focus();
                }
                else
                    this.Close();

            }
            else
            {
                if (validationError.Message.Length > 0)
                {
                    MsgHelper.MsgWarning(validationError.Message);
                    base.SetFocusObject(validationError.PropertyName, this);
                }
                else
                    MsgHelper.MsgUpdateError();
            }                
        }
    }
}
