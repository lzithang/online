using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class BandDiagnosisService : BaseService<BandDiagnosis>, IBandDiagnosisService
    {
		private IBandDiagnosisDal _bandDiagnosisDal { get; set; }
		public BandDiagnosisService(IBandDiagnosisDal bandDiagnosisDal)
		{
			_bandDiagnosisDal = bandDiagnosisDal;
			BaseDal = bandDiagnosisDal;
		}


	}
}
