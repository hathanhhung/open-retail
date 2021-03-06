/**
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;
using OpenRetail.Model;
using OpenRetail.Bll.Api;
using OpenRetail.Repository.Api;
using OpenRetail.Repository.Service;
 
namespace OpenRetail.Bll.Service
{    
    public class GajiKaryawanBll : IGajiKaryawanBll
    {
		private ILog _log;
		private GajiKaryawanValidator _validator;

		public GajiKaryawanBll(ILog log)
        {
			_log = log;
            _validator = new GajiKaryawanValidator();
        }

        public string GetLastNota()
        {
            var lastNota = string.Empty;

            using (IDapperContext context = new DapperContext())
            {
                IUnitOfWork uow = new UnitOfWork(context, _log);
                lastNota = uow.GajiKaryawanRepository.GetLastNota();
            }

            return lastNota;
        }

        public GajiKaryawan GetByID(string id)
        {
            GajiKaryawan obj = null;
            
            using (IDapperContext context = new DapperContext())
            {
                IUnitOfWork uow = new UnitOfWork(context, _log);
                obj = uow.GajiKaryawanRepository.GetByID(id);
            }

            return obj;
        }

        public IList<GajiKaryawan> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public IList<GajiKaryawan> GetAll()
        {
            throw new NotImplementedException();
        }

        public IList<GajiKaryawan> GetByBulanAndTahun(int bulan, int tahun)
        {
            IList<GajiKaryawan> oList = null;

            using (IDapperContext context = new DapperContext())
            {
                IUnitOfWork uow = new UnitOfWork(context, _log);
                oList = uow.GajiKaryawanRepository.GetByBulanAndTahun(bulan, tahun);
            }

            return oList;
        }

		public int Save(GajiKaryawan obj)
        {
            var result = 0;

            using (IDapperContext context = new DapperContext())
            {
                IUnitOfWork uow = new UnitOfWork(context, _log);
                result = uow.GajiKaryawanRepository.Save(obj);
            }

            return result;
        }

        public int Save(GajiKaryawan obj, ref ValidationError validationError)
        {
			var validatorResults = _validator.Validate(obj);

            if (!validatorResults.IsValid)
            {
                foreach (var failure in validatorResults.Errors)
                {
                    validationError.Message = failure.ErrorMessage;
                    validationError.PropertyName = failure.PropertyName;
                    return 0;
                }
            }

            return Save(obj);
        }

		public int Update(GajiKaryawan obj)
        {
            var result = 0;

            using (IDapperContext context = new DapperContext())
            {
                IUnitOfWork uow = new UnitOfWork(context, _log);
                result = uow.GajiKaryawanRepository.Update(obj);
            }

            return result;
        }

        public int Update(GajiKaryawan obj, ref ValidationError validationError)
        {
            var validatorResults = _validator.Validate(obj);

            if (!validatorResults.IsValid)
            {
                foreach (var failure in validatorResults.Errors)
                {
                    validationError.Message = failure.ErrorMessage;
                    validationError.PropertyName = failure.PropertyName;
                    return 0;
                }
            }

            return Update(obj);
        }

        public int Delete(GajiKaryawan obj)
        {
            var result = 0;

            using (IDapperContext context = new DapperContext())
            {
                IUnitOfWork uow = new UnitOfWork(context, _log);
                result = uow.GajiKaryawanRepository.Delete(obj);
            }

            return result;
        }        
    }
}     
