using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Reflection;

namespace Sqltry
{
    public class ContactDetails
    {
        public static SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        public static string strPersonalQuery = "insert into Person(PERSONID,FIRSTNAME,LASTNAME,GENDER,DOB)values (@personid,@fname,@lname,@gender,@dob)";
        public static string strContactQuery = "insert into Contact(PERSONID,EMAIL,MOBILE,PHONE,CONTACTTYPE)values (@personid,@email,@mobile,@phone,@contacttype)";
        public static string strAddressQuery = "insert into Address(PERSONID,ADDRESSTYPE,DOORNO,STREET,CITY,DISTRICT,STATE,COUNTRY,PINCODE)values (@personid,@addresstype,@doorno,@street,@city,@district,@state,@country,@pincode)";
        public static SqlCommand PersonalAddQuerycmd = new SqlCommand(strPersonalQuery, Conn);
        public static SqlCommand ContactAddQueryCmd = new SqlCommand(strContactQuery, Conn);
        public static SqlCommand AddressAddQueryCmd = new SqlCommand(strAddressQuery, Conn);
        public static SqlCommand PersonalQueryCmd = new SqlCommand("Select * from Person", Conn);
        public static SqlCommand ContactQueryCmd = new SqlCommand("Select * from Contact", Conn);
        public static SqlCommand AddressQueryCmd = new SqlCommand("Select * from Address", Conn);
        public static bool res;
        public static void PersonalDetailsDisplay(int nPersonId) 
        {
            bool Search = false;
            using (SqlDataReader reader = PersonalQueryCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (nPersonId.Equals(Convert.ToInt32(reader["PERSONID"])))
                    {
                        Search = true;
                        Console.WriteLine(String.Format("ID: {0} \t FIRSTNAME: {1} \t LASTNAME: {2} \t GENDER: {3} \t DOB: {4}", reader[0], reader[1], reader[2], reader[3], reader[4]));
                    }
                }
            }
            if (Search == false)
            {
                Console.WriteLine("User Not Found ! :(");
            }
        }
        public static void ContactDetailsDisplay(int nPersonId) 
        {
            using (SqlDataReader reader = ContactQueryCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (nPersonId.Equals(Convert.ToInt32(reader["PERSONID"])))
                    {
                        Console.Write("\t Contact : \n");
                        Console.WriteLine(String.Format("\t CONTACTTYPE: {1}\t EMAIL: {2} \t MOBILE: {3} \t PHONE: {4}", reader[0], reader[1], reader[2], reader[3], reader[4]));
                    }
                }
            }
        }
        public static void AddressDetailsDisplay(int nPersonId)
        {
            using (SqlDataReader reader = AddressQueryCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (nPersonId.Equals(Convert.ToInt32(reader["PERSONID"])))
                    {
                        Console.Write("\t ADDTESS : \n");
                        Console.WriteLine(String.Format("\t ADDRESSTYPE: {1} \n\t DOORNUMBER: {2} \t STREET: {3} \t CITY: {4} \t DISTRICT: {5} \t STATE :{6} \t COUNTRY : {7} \t PINCODE : {8}", reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6], reader[7], reader[8]));
                    }
                }
            }
        }
        public static void CreateContact()
        {
            Console.WriteLine("Enter the ID ");
            int nPersonId = Convert.ToInt32( Console.ReadLine());
            PersonalAddQuerycmd.Parameters.AddWithValue("@personid", nPersonId);
            ContactAddQueryCmd.Parameters.AddWithValue("@personid", nPersonId);
            AddressAddQueryCmd.Parameters.AddWithValue("@personid", nPersonId);
            Console.WriteLine("Enter Firstname :");
            PersonalAddQuerycmd.Parameters.AddWithValue("@fname", Console.ReadLine());
            Console.WriteLine("Enter Lastname :");
            PersonalAddQuerycmd.Parameters.AddWithValue("@lname", Console.ReadLine());
            Console.WriteLine("Enter Gender :");
            PersonalAddQuerycmd.Parameters.AddWithValue("@gender", Console.ReadLine());
            Console.WriteLine("Enter Date of Birth :");
            PersonalAddQuerycmd.Parameters.AddWithValue("@dob", Console.ReadLine());
            PersonalAddQuerycmd.ExecuteNonQuery();
            Console.WriteLine("Select Contact Type : \n 1.Personal 2.Office");
            string strSelectContactType = Console.ReadLine();
            res = int.TryParse(strSelectContactType, out _);
            if (res == true)
            {
                switch (strSelectContactType)
                {
                    case "1":
                        ContactAddQueryCmd.Parameters.AddWithValue("@contacttype", "Personal");
                        break;
                    case "2":
                        ContactAddQueryCmd.Parameters.AddWithValue("@contacttype", "Office");
                        break;
                    default:
                        Console.WriteLine("Invalid number !! :(");
                        break;
                }
            }
            Console.WriteLine("Enter Email :");
            ContactAddQueryCmd.Parameters.AddWithValue("@email", Console.ReadLine());
            Console.WriteLine("Enter MobileNO :");
            ContactAddQueryCmd.Parameters.AddWithValue("@mobile", Console.ReadLine());
            Console.WriteLine("Enter PhoneNO :");
            ContactAddQueryCmd.Parameters.AddWithValue("@phone", Console.ReadLine());
            ContactAddQueryCmd.ExecuteNonQuery();
            Console.WriteLine("Select AddressType : \n 1.Permanent 2.Residential");
            string strSelectAddressType = Console.ReadLine();
            res = int.TryParse(strSelectAddressType, out _);
            if (res == true)
            {
                switch (strSelectAddressType)
                {
                    case "1":
                        AddressAddQueryCmd.Parameters.AddWithValue("@addresstype", "Permanent");
                        break;
                    case "2":
                        AddressAddQueryCmd.Parameters.AddWithValue("@addresstype", "Residential");
                        break;
                    default:
                        Console.WriteLine("Invalid number !! :(");
                        break;
                }
            }
            Console.WriteLine("Enter DoorNo :");
            AddressAddQueryCmd.Parameters.AddWithValue("@doorno", Console.ReadLine());
            Console.WriteLine("Enter Street :");
            AddressAddQueryCmd.Parameters.AddWithValue("@street", Console.ReadLine());
            Console.WriteLine("Enter City :");
            AddressAddQueryCmd.Parameters.AddWithValue("@city", Console.ReadLine());
            Console.WriteLine("Enter District :");
            AddressAddQueryCmd.Parameters.AddWithValue("@district", Console.ReadLine());
            Console.WriteLine("Enter State :");
            AddressAddQueryCmd.Parameters.AddWithValue("@state", Console.ReadLine());
            Console.WriteLine("Enter Contry :");
            AddressAddQueryCmd.Parameters.AddWithValue("@country", Console.ReadLine());
            Console.WriteLine("Enter Pincode :");
            AddressAddQueryCmd.Parameters.AddWithValue("@pincode", Console.ReadLine());
            AddressAddQueryCmd.ExecuteNonQuery();
            Console.WriteLine("Student registeration Successfully!!!thank you");
            AddMore();
        }
        public static void ReadContact()
        {
            Console.WriteLine("Done.");
            using (SqlDataReader reader = PersonalQueryCmd.ExecuteReader())
            {
                Console.WriteLine("Person Details Table : ");
                while (reader.Read())
                {
                    Console.WriteLine(String.Format("ID: {0} \t FIRSTNAME: {1} \t LASTNAME: {2} \t GENDER: {3} \t DOB: {4}", reader[0], reader[1], reader[2], reader[3], reader[4]));
                }
            }
            using (SqlDataReader reader = ContactQueryCmd.ExecuteReader())
            {
                Console.WriteLine("Contact Details Table : ");
                while (reader.Read())
                {
                    Console.WriteLine(String.Format("ID: {0} \t CONTACTTYPE: {1} \t EMAIL: {2} \t MOBILE: {3} \t PHONE: {4}", reader[0], reader[1], reader[2], reader[3], reader[4]));
                }
            }
            using (SqlDataReader reader = AddressQueryCmd.ExecuteReader())
            {
                Console.WriteLine("Address Details Table : ");
                while (reader.Read())
                {
                    Console.WriteLine(String.Format("ID: {0} \t Address Type : {1} \t DoorNo: {2} \t Street: {3} \t City: {4} \t District: {5} \t State: {6} \t Country: {7} \t Pincode: {8} \t", reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6], reader[7], reader[8]));
                }
            }
        }
        public static void UpdateContact()
        {
            SqlCommand PersonalQueryUpate = new SqlCommand("UPDATE Person SET FIRSTNAME=@Dfname, LASTNAME=@lname, GENDER=@gender ,DOB=@date WHERE PERSONID=@personid", Conn);
            SqlCommand ContactQueryUpdate = new SqlCommand("UPDATE Contact SET EMAIL=@email, MOBILE=@mobile, PHONE=@phone  WHERE PERSONID=@personid AND CONTACTTYPE=@contacttype", Conn);
            SqlCommand AddressQueryUpdate = new SqlCommand("UPDATE Address SET ADDRESSTYPE=@addresstype,DOORNO=@doorno, STREET=@street, CITY=@city ,DISTRICT=@district ,STATE=@state,COUNTRY=@country, PINCODE=@pincode WHERE PERSONID=@personid AND ADDRESSTYPE=@addresstype ", Conn);
            Console.WriteLine("Enter the ID number to UPDATE: \n");
            int nPersonid = Convert.ToInt32(Console.ReadLine());
            PersonalQueryUpate.Parameters.AddWithValue("@personid", nPersonid);
            ContactQueryUpdate.Parameters.AddWithValue("@personid", nPersonid);
            AddressQueryUpdate.Parameters.AddWithValue("@personid", nPersonid);
            Console.WriteLine("    UPDATE \n1.PersonalDetails\n2.contactDEtails\n3.AddressDEtails:"  + " \n    ________________");
            string strSelect = Console.ReadLine();
            res = int.TryParse(strSelect, out _);
            if (res == true)
            {
                switch (strSelect)
                {
                    case "1":
                        Console.WriteLine("\n    Update PersonalDetails : \n");
                        PersonalDetailsDisplay(nPersonid);
                        Console.WriteLine("Enter Updated FirstName :");
                        PersonalQueryUpate.Parameters.AddWithValue("@Dfname", Console.ReadLine());
                        Console.WriteLine("Enter Updated LastName :");
                        PersonalQueryUpate.Parameters.AddWithValue("@lname", Console.ReadLine());
                        Console.WriteLine("Enter Updated Gender  :");
                        PersonalQueryUpate.Parameters.AddWithValue("@gender", Console.ReadLine());
                        Console.WriteLine("Enter  Updated Date of Birth  :");
                        PersonalQueryUpate.Parameters.AddWithValue("@date", Console.ReadLine());
                        PersonalQueryUpate.ExecuteNonQuery();
                        Console.WriteLine("\n    Personal Details updated !! :) ");
                        break;
                    case "2":
                        Console.WriteLine("\n    Update ContactDetails : \n");
                        ContactDetailsDisplay(nPersonid);
                        Console.WriteLine("Enter Contact Type : \n 1.Personal 2.Office");
                        string strSelectContactType = Console.ReadLine();
                        res = int.TryParse(strSelectContactType, out _);
                        if (res == true)
                        {
                            switch (strSelectContactType)
                            {
                                case "1":
                                    ContactQueryUpdate.Parameters.AddWithValue("@contacttype", "Personal");
                                    break;
                                case "2":
                                    ContactQueryUpdate.Parameters.AddWithValue("@contacttype", "Office");
                                    break;
                                default:
                                    Console.WriteLine("Invalid number !! :(");
                                    break;
                            }
                        }
                        Console.WriteLine("Enter Email");
                        ContactQueryUpdate.Parameters.AddWithValue("@email", Console.ReadLine());
                        Console.WriteLine("Enter Mobile");
                        ContactQueryUpdate.Parameters.AddWithValue("@mobile", Console.ReadLine());
                        Console.WriteLine("Enter Phone");
                        ContactQueryUpdate.Parameters.AddWithValue("@phone", Console.ReadLine());
                        ContactQueryUpdate.ExecuteNonQuery();
                        Console.WriteLine("\n    Contact Details updated !! :) ");
                        break;
                    case "3":
                        Console.WriteLine("\n    update AddressDetails : ");
                        AddressDetailsDisplay(nPersonid);
                        Console.WriteLine("1.Permanent\n2.Residential\nEnter your choise: ");
                        string strSelect1 = Console.ReadLine();
                        switch (strSelect1)
                        {
                            case "1":
                                AddressQueryUpdate.Parameters.AddWithValue("@addresstype", "Permanent");
                                break; 
                            case "2":
                                AddressQueryUpdate.Parameters.AddWithValue("@addresstype", "Residential");
                                break;
                            default:
                                Console.WriteLine("invalide number !!");
                                break;
                        }
                        Console.WriteLine("Enter DoorNumber");
                        AddressQueryUpdate.Parameters.AddWithValue("@doorno", Console.ReadLine());
                        Console.WriteLine("Enter Street");
                        AddressQueryUpdate.Parameters.AddWithValue("@street", Console.ReadLine());
                        Console.WriteLine("Enter City");
                        AddressQueryUpdate.Parameters.AddWithValue("@city", Console.ReadLine());
                        Console.WriteLine("Enter  District");
                        AddressQueryUpdate.Parameters.AddWithValue("@district", Console.ReadLine());
                        Console.WriteLine("Enter  State");
                        AddressQueryUpdate.Parameters.AddWithValue("@state", Console.ReadLine());
                        Console.WriteLine("Enter  Country");
                        AddressQueryUpdate.Parameters.AddWithValue("@country", Console.ReadLine());
                        Console.WriteLine("Enter  Pincode");
                        AddressQueryUpdate.Parameters.AddWithValue("@pincode", Console.ReadLine());
                        AddressQueryUpdate.ExecuteNonQuery();
                        Console.WriteLine(" Address Details updated !! :)");
                        break;
                    default:
                        Console.WriteLine("Invalid number!! please enter the correct number  ");
                        break;
                }
                ReadContact();
            }
        }
        public static void DeleteContact()
        {
            string strDeleteQuery = " DELETE FROM Person WHERE PERSONID =@idnumber ";
            SqlCommand DeleteCmd = new SqlCommand(strDeleteQuery, Conn);
            Console.WriteLine("Enter the ID name to delete :");
            int nPersonId = Convert.ToInt32(Console.ReadLine());
            DeleteCmd.Parameters.AddWithValue("@idnumber", nPersonId);
            DeleteCmd.ExecuteNonQuery();
            ReadContact();
        }
        private static void SearchContact()
        {
            Console.WriteLine("ENTER THE PERSONID TO SEARCH  :");
            int nPersonId = Convert.ToInt32(Console.ReadLine());
            PersonalDetailsDisplay(nPersonId);
            ContactDetailsDisplay(nPersonId);
            AddressDetailsDisplay(nPersonId);
        }
        public static void AddMore()
        {
            try
            {
                Console.WriteLine("ADD More Details :\nEnter the ID ");
                int nPersonId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(" \n\t1.Contact \n\t2.ADDRESS ");
                string strSelect = Console.ReadLine();
                res = int.TryParse(strSelect, out _);
                if (res == true)
                {
                    switch (strSelect)
                    {
                        case "1":
                            ContactAddQueryCmd.Parameters.AddWithValue("@personid", nPersonId);
                            Console.WriteLine("Enter Contact Type : \n 1.Personal 2.Office");
                            string strSelectContactType = Console.ReadLine();
                            res = int.TryParse(strSelectContactType, out _);
                            if (res == true)
                            {
                                switch (strSelectContactType)
                                {
                                    case "1":
                                        ContactAddQueryCmd.Parameters.AddWithValue("@contacttype", "Personal");
                                        break;
                                    case "2":
                                        ContactAddQueryCmd.Parameters.AddWithValue("@contacttype", "Office");
                                        break;
                                    default:
                                        Console.WriteLine("Invalid number !! :(");
                                        break;
                                }
                            }
                            Console.WriteLine("Enter Email :");
                            ContactAddQueryCmd.Parameters.AddWithValue("@email", Console.ReadLine());
                            Console.WriteLine("Enter MobileNO :");
                            ContactAddQueryCmd.Parameters.AddWithValue("@mobile", Console.ReadLine());
                            Console.WriteLine("Enter PhoneNO :");
                            ContactAddQueryCmd.Parameters.AddWithValue("@phone", Console.ReadLine());
                            ContactAddQueryCmd.ExecuteNonQuery();
                            Console.WriteLine(" \n\tContact successfully added :)");
                            break;
                        case "2":
                            AddressAddQueryCmd.Parameters.AddWithValue("@personid", nPersonId);
                            Console.WriteLine("Select AddressType : \n 1.Permanent 2.Residential");
                            string strSelectAddressType = Console.ReadLine();
                            res = int.TryParse(strSelectAddressType, out _);
                            if (res == true)
                            {
                                switch (strSelectAddressType)
                                {
                                    case "1":
                                        AddressAddQueryCmd.Parameters.AddWithValue("@addresstype", "Permanent");
                                        break;
                                    case "2":
                                        AddressAddQueryCmd.Parameters.AddWithValue("@addresstype", "Residential");
                                        break;
                                    default:
                                        Console.WriteLine("Invalid number !! :(");
                                        break;
                                }
                            }
                            Console.WriteLine("Enter DoorNo :");
                            AddressAddQueryCmd.Parameters.AddWithValue("@doorno", Console.ReadLine());
                            Console.WriteLine("Enter Street :");
                            AddressAddQueryCmd.Parameters.AddWithValue("@street", Console.ReadLine());
                            Console.WriteLine("Enter City :");
                            AddressAddQueryCmd.Parameters.AddWithValue("@city", Console.ReadLine());
                            Console.WriteLine("Enter District :");
                            AddressAddQueryCmd.Parameters.AddWithValue("@district", Console.ReadLine());
                            Console.WriteLine("Enter State :");
                            AddressAddQueryCmd.Parameters.AddWithValue("@state", Console.ReadLine());
                            Console.WriteLine("Enter Contry :");
                            AddressAddQueryCmd.Parameters.AddWithValue("@country", Console.ReadLine());
                            Console.WriteLine("Enter Pincode :");
                            AddressAddQueryCmd.Parameters.AddWithValue("@pincode", Console.ReadLine());
                            AddressAddQueryCmd.ExecuteNonQuery();
                            Console.WriteLine(" \n\tAddress successfully added :)");
                            break;
                        default:
                            Console.WriteLine("invalid number !!");
                            break;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Error");
            }
        }
        public static void Main(string[] args)
        {
            while (true)
            {
                Conn.Open();
                Console.WriteLine(" CONTACT BOOk\n ____________\n");
                Console.WriteLine(" 1.CREATE CONTACT \n 2.READ TOTAL CONTACT \n 3.UPDATE CONTACT \n 4.DELETE CONTACT\n 5.SEARCH CONTACT  \n 6.ADD ADDRESS(or) CONTACT DETAILS \n 7.EXIT\n  Enter your choice: \n  __________________");
                string number = Console.ReadLine();
                res = int.TryParse(number, out _);
                if (res == true)
                {
                    switch (number)
                    {
                        case "1":
                            CreateContact();
                            Conn.Close();
                            break;
                        case "2":
                            ReadContact();
                            Conn.Close();
                            break;
                        case "3":
                            UpdateContact();
                            Conn.Close();
                            break;
                        case "4":
                            DeleteContact();
                            Conn.Close();
                            break;
                        case "5":
                            SearchContact();
                            Conn.Close();
                            break;
                        case "6":
                            AddMore();
                            Conn.Close();
                            break;
                        case "7":
                            Conn.Close();
                            return;
                        default:
                            Console.WriteLine("Invalid number!! please enter the correct number  ");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("This is not a number!! please enter the  number ! \n");
                }
                Console.ReadLine();
            }
        }
    }
}

