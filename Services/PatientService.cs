using PluralHealthDemoApp.Models;

namespace PluralHealthDemoApp.Services
{
    public class PatientService
    {
        static List<Patient> Patients { get; }

        static PatientService()
        {
            Patients = [
                    new Patient {Code = "P001", Name = "Adenike", Email="adenike@gmail.com",PhoneNo= "08023118585",CreatedDate = DateTime.Now },
                    new Patient {Code = "P002", Name = "Enoch", Email="enoch@gmail.com",PhoneNo="080221192912", WalletBalance=9000, CreatedDate = DateTime.Now },
                    new Patient {Code = "P003", Name = "John", Email="john@gmail.com",PhoneNo="080221199912", WalletBalance=1000, CreatedDate = DateTime.Now },
                    new Patient {Code = "P004", Name = "Kehinde", Email="kehinde@gmail.com",PhoneNo="080221292912", WalletBalance=2000, CreatedDate = DateTime.Now },
                    new Patient {Code = "P005", Name = "Olumide", Email="olumide@gmail.com",PhoneNo="07038738314", CreatedDate = DateTime.Now },
                    new Patient {Code = "P006", Name = "Alex", Email="alex@gmail.com",PhoneNo="07038735314",WalletBalance=2500, CreatedDate = DateTime.Now },
                ];
        }

        public static List<Patient> GetAll() => Patients;

        public static Patient? Get(string code) => Patients.FirstOrDefault(p=>p.Code==code);
    }
}
