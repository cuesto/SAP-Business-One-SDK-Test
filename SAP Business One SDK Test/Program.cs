using Microsoft.SqlServer.Server;
using SAPbobsCOM;
using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAP_Business_One_SDK_Test
{
    class Program
    {
        public static SAPbobsCOM.ICompany oCompany;

        static void Main(string[] args)
        {
            if (InitializeCompany())
            {
                GetProduct("A0");
            }

        }

        public static bool InitializeCompany()
        {
            // Create a new company object
            oCompany = new SAPbobsCOM.Company();

            if (oCompany.Connected)
            {
                oCompany.Disconnect();
            }

            // SQL Server
            oCompany.Server = "";
            // server version
            oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2017;
            // usuario de la base de datos
            oCompany.DbUserName = "";
            // password de la base de datos
            oCompany.DbPassword = "";
            // base de datos de la compania
            oCompany.CompanyDB = "";
            // usuario de SAP B1
            oCompany.UserName = "";
            // password de SAP B1
            oCompany.Password = "";
            // conexion segur
            oCompany.UseTrusted = false;
            // license server
            oCompany.LicenseServer = "";

            // conexion a la compania
            int lRetCode = oCompany.Connect();

            // si errCode es diferente de 0 pudo haber algún error
            if (lRetCode != 0)
            {
                var a = Program.GetLastError("Company: " + oCompany.Server + ", DB: " + oCompany.CompanyDB);
            }

            var isConnected = oCompany.Connected;


            return isConnected;
        }

        public static string GetLastError(string errTitle = "")
        {
            int temp_int = Program.oCompany.GetLastErrorCode();
            return Program.oCompany.GetLastErrorDescription();
        }

        public static void GetProduct(string ItemCode)
        {
            var product = (SAPbobsCOM.Items)oCompany.GetBusinessObject(BoObjectTypes.oItems);

            product.GetByKey(ItemCode);

            product.ItemName = "Apple";

            int lRetCode = product.Update();

            // si errCode es diferente de 0 pudo haber algún error
            if (lRetCode != 0)
            {
                var a = Program.GetLastError("Company: " + oCompany.Server + ", DB: " + oCompany.CompanyDB);
            }

        }
    }
}
