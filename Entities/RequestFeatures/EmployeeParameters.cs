namespace Entities.RequestFeatures
{
    public class EmployeeParameters : RequestParameters
    {
        public EmployeeParameters() 
        {
            OrderBy = "FirstName";
        }

        public uint MinAge { get; set; }

        public uint MaxAge { get; set; }

        public bool ValidAgeRange => MaxAge > MinAge;

        public string SearchTerm { get; set; }
    }
}
