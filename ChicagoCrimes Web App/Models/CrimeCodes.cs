//
// One crime
//

namespace crimes.Models
{

  public class CrimeCodes
	{
	
		// data members with auto-generated getters and setters:
	   public string IUCR { get; set; }
		public string PrimaryDesc { get; set; }
      public string SecondaryDesc {get;set; }
		public int TotalNumOfCrimes { get; set; }
		
	
		// default constructor:
		public CrimeCodes()
      {}
     
		
		
		// constructor:
		public CrimeCodes(string IUCR, string PrimaryDesc, string SecondaryDesc, int TotalNumOfCrimes)
		{
			IUCR = this.IUCR;
			PrimaryDesc = this.PrimaryDesc;
			SecondaryDesc = this.SecondaryDesc;
			TotalNumOfCrimes = this.TotalNumOfCrimes;
		}
		
	}//class

}//namespace