using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Core;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace GruziVezi
{
    /// <summary>
    /// Логика взаимодействия для WOperator.xaml
    /// </summary>

    public class CustomOrders
    {
        public string customSenders { get; set; }
        public string customDrivers { get; set; }
        public string customRoutes { get; set; }
        public Orders order { get; set; }
    }

    public class CustomRoutes
    {       
        public string customRoutes { get; set; }
        public Routes route { get; set; }
    }

    public class CustomDrivers
    {
        public string customDrivers{ get; set; }
        public Drivers driver { get; set; }

    }

    public class CustomSenders
    {
        public string customSenders { get; set; }
        public Senders sender { get; set; }
    }

    public partial class WOperator : System.Windows.Window
    {
        public WOperator()
        {
            InitializeComponent();
            this.Title += WAuthorization.user.surname + " " + WAuthorization.user.name[0] + "." + WAuthorization.user.middlename[0] + ".";

            ////Заказы
            LoadDGOrders();
            LoadCBSavedRoutes();
            LoadCBCities();
            LoadCBDrivers();
            LoadCBStatus();
            LoadCBSenders();

            ////Заказчики
            LoadDGSenders();
            LoadCBCompany();

            ////Компании
            LoadDGCompany();

            //Города
            LoadDGCities();
        }


        public void UpdateAll()
        {            
            ////Заказы
            LoadDGOrders();
            LoadCBSavedRoutes();
            LoadCBCities();
            LoadCBDrivers();
            LoadCBStatus();
            LoadCBSenders();

            ////Заказчики
            LoadDGSenders();
            LoadCBCompany();

            ////Компании
            LoadDGCompany();

            //Города
            LoadDGCities();
            orders = new List<CustomOrders>();
            senders = new List<Senders>();
            companys = new List<Company>();
            cities = new List<Cities>();
        }
        List<CustomOrders> orders;
        List<Senders> senders;
        List<Company> companys;
        List<Cities> cities;
        /********************************************/
        /*                 Заказы                   */
        /********************************************/
        /*ГОТОВО*/

        private void LoadDGOrders()
        {
            GruziVeziEntities db = new GruziVeziEntities();

            List<CustomOrders> list = new List<CustomOrders>();

            foreach (Orders item in db.Orders.ToList())
            {
                CustomOrders custom = new CustomOrders();
                custom.customSenders = item.Senders.surname + " " + item.Senders.name[0] + "." + item.Senders.middlename[0];
                custom.customDrivers = item.Drivers.surname + " " + item.Drivers.name[0] + "." + item.Drivers.middlename[0];
                custom.customRoutes = item.Routes.CitiesS.name + " - " + item.Routes.CitiesE.name;
                custom.order = item;

                list.Add(custom);
            }


            dgOrders.ItemsSource = list;
            orders = list;
           
        }
        private void LoadCBSenders()
        {
            GruziVeziEntities db = new GruziVeziEntities();

            List<CustomSenders> list = new List<CustomSenders>();

            foreach (Senders item in db.Senders.ToList())
            {
                CustomSenders custom = new CustomSenders();
                custom.customSenders = item.surname + " " + item.name[0] + "." + item.middlename[0] + " | " + item.Company.name;
                custom.sender = item;

                list.Add(custom);
            }


            cbSenders.ItemsSource = list;
        }

        private void LoadCBDrivers()
        {
            GruziVeziEntities db = new GruziVeziEntities();

            List<CustomDrivers> list = new List<CustomDrivers>();

            foreach (Drivers item in db.Drivers.ToList())
            {
                CustomDrivers custom = new CustomDrivers();
                custom.customDrivers = item.surname + " " + item.name[0] + "." + item.middlename[0];
                custom.driver = item;

                list.Add(custom);
            }


            cbDrivers.ItemsSource = list;
        }


        private void LoadCBStatus()
        {
            GruziVeziEntities db = new GruziVeziEntities();
            cbStatus.ItemsSource = db.Status.ToList();
        }

        private void LoadCBCities()
        {
            GruziVeziEntities db = new GruziVeziEntities();
            cbCityStart.ItemsSource = cbCityEnd.ItemsSource = db.Cities.ToList();
            //cbCityEnd.ItemsSource = db.Cities.ToList();
        }


        private void LoadCBSavedRoutes()
        {
            GruziVeziEntities db = new GruziVeziEntities();

            List<CustomRoutes> list = new List<CustomRoutes>();

            foreach (Routes item in db.Routes.ToList())
            {
                CustomRoutes custom = new CustomRoutes();
                custom.customRoutes = item.CitiesS.name + "-" + item.adressStart + "->" + item.CitiesE.name + "-" + item.adressEnd;
                custom.route = item;

                list.Add(custom);
            }


            cbSavedRoutes.ItemsSource = list;

        }

        private void ClearCBSavedRoutes(object sender, SelectionChangedEventArgs e)
        {
            cbSavedRoutes.SelectedItem = null;
        }
        private void ClearTBRoutes(object sender, SelectionChangedEventArgs e)
        {
            cbCityEnd.SelectedItem = null;
            cbCityStart.SelectedItem = null;
            tbAdressEnd.Text = null;
            tbAdressStart.Text = null;
        }

        private void ClearTBOrders()
        {
            cbSenders.SelectedItem = null;
            cbDrivers.SelectedItem = null;
            tbDescription.Text = null;

            cbCityEnd.SelectedItem = null;
            cbCityStart.SelectedItem = null;
            tbAdressEnd.Text = null;
            tbAdressStart.Text = null;
        }


        private void FillTBOrder(object sender, RoutedEventArgs e)
        {
            if ((dgOrders.SelectedItem as CustomOrders) != null)
            {

                foreach (CustomSenders item in cbSenders.ItemsSource)
                {
                    if (item.sender.id == (dgOrders.SelectedItem as CustomOrders).order.id_Sender)
                    {
                        cbSenders.SelectedItem = item;
                    }
                }

                foreach (CustomDrivers item in cbDrivers.ItemsSource)
                {
                    if (item.driver.id == (dgOrders.SelectedItem as CustomOrders).order.id_Driver)
                    {
                        cbDrivers.SelectedItem = item;
                    }
                }

                tbDescription.Text = (dgOrders.SelectedItem as CustomOrders).order.description;


   

                foreach (Cities item in cbCityStart.ItemsSource)
                {
                    if (item.id == (dgOrders.SelectedItem as CustomOrders).order.Routes.id_CityStart)
                    {
                        cbCityStart.SelectedItem = item;
                    }
                }

                foreach (Cities item in cbCityEnd.ItemsSource)
                {
                    if (item.id == (dgOrders.SelectedItem as CustomOrders).order.Routes.id_CityEnd)
                    {
                        cbCityEnd.SelectedItem = item;
                    }
                }

                tbAdressStart.Text = (dgOrders.SelectedItem as CustomOrders).order.Routes.adressStart;
                tbAdressEnd.Text = (dgOrders.SelectedItem as CustomOrders).order.Routes.adressEnd;

                foreach (Status item in cbStatus.ItemsSource)
                {
                    if (item.id == (dgOrders.SelectedItem as CustomOrders).order.id_Status)
                    {
                        cbCityEnd.SelectedItem = item;
                    }
                }


            }

        }


        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {

            Routes route = new Routes();

            if (cbSavedRoutes.SelectedItem == null)
            {
                Cities citiesS = cbCityStart.SelectedItem as Cities;
                Cities citiesE = cbCityEnd.SelectedItem as Cities;


                if (RoutesTable.Add(citiesS.id, tbAdressStart.Text, citiesE.id, tbAdressEnd.Text, out route))
                {
                    LoadCBSavedRoutes();

                    cbCityEnd.SelectedItem = null;
                    cbCityStart.SelectedItem = null;
                    tbAdressEnd.Text = null;
                    tbAdressStart.Text = null;

                    cbSavedRoutes.SelectedItem = route;
                }
            }

            route = (cbSavedRoutes.SelectedItem as CustomRoutes).route;
            


            if (OrdersTable.Add((cbSenders.SelectedItem as CustomSenders) !=null?(cbSenders.SelectedItem as CustomSenders).sender.id:0, (cbDrivers.SelectedItem as CustomDrivers)!=null? (cbDrivers.SelectedItem as CustomDrivers).driver.id:0, route!=null?route.id:0, tbDescription.Text, (cbStatus.SelectedItem as Status)!=null? (cbStatus.SelectedItem as Status).id:0, WAuthorization.user.id))
            {
                LoadDGOrders();
                ClearTBOrders();
                UpdateAll();
            }
        }

        private void btnUpdateOrder_Click(object sender, RoutedEventArgs e)
        {
     
            
            Routes route = new Routes();

            if (cbSavedRoutes.SelectedItem == null)
            {
                Cities citiesS = cbCityStart.SelectedItem as Cities;
                Cities citiesE = cbCityEnd.SelectedItem as Cities;


                if (RoutesTable.Update((dgOrders.SelectedItem as CustomOrders) != null ? (dgOrders.SelectedItem as CustomOrders).order.id_Route : 0, citiesS.id, tbAdressStart.Text, citiesE.id, tbAdressEnd.Text, out route))
                {
                    LoadCBSavedRoutes();

                    cbCityEnd.SelectedItem = null;
                    cbCityStart.SelectedItem = null;
                    tbAdressEnd.Text = null;
                    tbAdressStart.Text = null;

                    foreach (CustomRoutes item in cbSavedRoutes.ItemsSource)
                    {
                        if (item.route.id == route.id)
                        {
                            cbSavedRoutes.SelectedItem = item;
                            break;
                        }
                    }

                   
                }
            }

            route = (cbSavedRoutes.SelectedItem as CustomRoutes).route;



            if (OrdersTable.Update((dgOrders.SelectedItem as CustomOrders)!=null?(dgOrders.SelectedItem as CustomOrders).order.id:0, (cbSenders.SelectedItem as CustomSenders) != null ? (cbSenders.SelectedItem as CustomSenders).sender.id : 0, (cbDrivers.SelectedItem as CustomDrivers) != null ? (cbDrivers.SelectedItem as CustomDrivers).driver.id : 0, route != null ? route.id : 0, tbDescription.Text, (cbStatus.SelectedItem as Status) != null ? (cbStatus.SelectedItem as Status).id : 0, WAuthorization.user.id))
            {
                LoadDGOrders();
                ClearTBOrders();
            }
        }

        /********************************************/
        /*                 Заказчики                */
        /********************************************/
        /*ДОБАВЛЕНИЕ / РЕДАКТИРОВАНИЕ */
        private void LoadDGSenders()
        {
            GruziVeziEntities db = new GruziVeziEntities();

            dgSenders.ItemsSource = db.Senders.ToList();
            senders = db.Senders.ToList();
        }

        private void LoadCBCompany()
        {
            GruziVeziEntities db = new GruziVeziEntities();

            cbCompany.ItemsSource = db.Company.ToList();
        }

        private void ClearTBSenders()
        {
            tbSurnameSenders.Text = null;
            tbNameSenders.Text = null;
            tbMiddlenameSenders.Text = null;
        }

        private void FillTBSender(object sender, RoutedEventArgs e)
        {
            if ((dgSenders.SelectedItem as Senders) != null)
            {

                foreach (Company item in cbCompany.ItemsSource)
                {
                    if (item.id == (dgSenders.SelectedItem as Senders).id_Company)
                    {
                        cbCompany.SelectedItem = item;
                    }
                }



                tbSurnameSenders.Text = (dgSenders.SelectedItem as Senders).surname;
                tbNameSenders.Text = (dgSenders.SelectedItem as Senders).name;
                tbMiddlenameSenders.Text = (dgSenders.SelectedItem as Senders).middlename;



            }

        }

        private void btnAddSender_Click(object sender, RoutedEventArgs e)
        {
            if (SendersTable.Add(tbSurnameSenders.Text,tbNameSenders.Text,tbMiddlenameSenders.Text,(cbCompany.SelectedItem as Company).id))
            {
                LoadDGSenders();
                LoadCBCompany();
                ClearTBSenders();
                UpdateAll();
            }
        }

        private void btnUpdateSender_Click(object sender, RoutedEventArgs e)
        {
            if((dgSenders.SelectedItem as Senders)!=null)
            if (SendersTable.Update((dgSenders.SelectedItem as Senders).id,tbSurnameSenders.Text, tbNameSenders.Text, tbMiddlenameSenders.Text, (cbCompany.SelectedItem as Company).id))
            {
                LoadDGSenders();
                LoadCBCompany();
                ClearTBSenders();
                 UpdateAll();
             }
        }

        /********************************************/
        /*                 Компании               */
        /********************************************/
        /*ДОБАВЛЕНИЕ / РЕДАКТИРОВАНИЕ */

        private void LoadDGCompany()
        {
            GruziVeziEntities db = new GruziVeziEntities();

            dgCompany.ItemsSource = db.Company.ToList();
            companys = db.Company.ToList();
        }

        private void FillTBCompany(object sender, RoutedEventArgs e)
        {
            if ((dgCompany.SelectedItem as Company) != null)
            {
                tbCompanyName.Text = (dgCompany.SelectedItem as Company).name;    
            }

        }
        private void ClearTBCompany()
        {
            tbCompanyName.Text = null;
 
        }

        private void btnAddCompany_Click(object sender, RoutedEventArgs e)
        {
            if(CompanyTable.Add(tbCompanyName.Text))
            {
                LoadDGCompany();
                ClearTBCompany();
                UpdateAll();

            }
        }
        private void btnUpdateCompany_Click(object sender, RoutedEventArgs e)
        {
            if ((dgCompany.SelectedItem as Company)!=null)
            {
                if (CompanyTable.Update((dgCompany.SelectedItem as Company).id, tbCompanyName.Text))
                {
                    LoadDGCompany();
                    ClearTBCompany();
                    UpdateAll();
                }
            }
        }

    


        /********************************************/
        /*                 Города            */
        /********************************************/
        /*ДОБАВЛЕНИЕ / РЕДАКТИРОВАНИЕ */


        private void LoadDGCities()
        {
            GruziVeziEntities db = new GruziVeziEntities();

            dgCities.ItemsSource = db.Cities.ToList();
            cities = db.Cities.ToList();
        }

        private void FillTBCity(object sender, RoutedEventArgs e)
        {
            if ((dgCities.SelectedItem as Cities) != null)
            {
                tbCityName.Text = (dgCities.SelectedItem as Cities).name;
            }

        }
        private void ClearTBCities()
        {
            tbCityName.Text = null;

        }

        private void btnAddCity_Click(object sender, RoutedEventArgs e)
        {
            if (CitiesTable.Add(tbCityName.Text))
            {
                LoadDGCities();
                ClearTBCities();
                UpdateAll();
            }
        }

        private void btnUpdateCity_Click(object sender, RoutedEventArgs e)
        {
            if ((dgCities.SelectedItem as Cities) != null)
            {
                if (CitiesTable.Update((dgCompany.SelectedItem as Cities).id, tbCityName.Text))
                {
                    LoadDGCities();
                    ClearTBCities();
                    UpdateAll();
                }
            }
        }

        private void btnOtchet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Word._Application wApp = new Word.Application();
                Word._Document wDoc = wApp.Documents.Add();
                wApp.Visible = true;
                wDoc.Activate();
                var ProductParagraph = wDoc.Content.Paragraphs.Add();
                //ProductParagraph.Range.Text = $"День недели:\t{dayOfTheWeek.Name}\n" + $"Статус:\t{shedule.Status}\n" + $"Время работы:\t{shedule.Duration}\n" + $"Цех:\t{shedule.Cabinet}\n";
                Word.Table wTable = wDoc.Tables.Add((Microsoft.Office.Interop.Word.Range)ProductParagraph.Range,
                orders.Count + 1, 4, Word.WdDefaultTableBehavior.wdWord9TableBehavior);
                wTable.Cell(1, 1).Range.Text = "Заказчик";
                wTable.Cell(1, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                wTable.Cell(1, 2).Range.Text = "Маршрут";
                wTable.Cell(1, 2).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                wTable.Cell(1, 3).Range.Text = "Водитель";
                wTable.Cell(1, 3).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                wTable.Cell(1, 4).Range.Text = "Статус";
                wTable.Cell(1, 4).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                int countRow = 2;
                foreach (var item in orders)
                {
                    wTable.Cell(countRow, 1).Range.Text = item.customSenders.ToString();
                    wTable.Cell(countRow, 1).Range.Paragraphs.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    wTable.Cell(countRow, 2).Range.Text = item.customRoutes.ToString();
                    wTable.Cell(countRow, 2).Range.Paragraphs.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    wTable.Cell(countRow, 3).Range.Text = item.customDrivers.ToString();
                    wTable.Cell(countRow, 3).Range.Paragraphs.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    wTable.Cell(countRow, 4).Range.Text = item.order.Status.name.ToString();
                    wTable.Cell(countRow, 4).Range.Paragraphs.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    countRow++;
                }
                /*Word.Chart wChart;
                Word.InlineShape inlineShape;
                inlineShape = wDoc.InlineShapes.AddChart(Microsoft.Office.Core.XlChartType.xlColumnClustered, ProductParagraph.Range);
                wChart = inlineShape.Chart;

                dynamic chartWB = wChart.ChartData.Workbook;
                dynamic chartTable = chartWB.Sheets[1].ListObjects("Таблица1"); chartTable.DataBodyRange.ClearContents();
                dynamic chartRange = chartTable.Range.Resize[2, dayOfTheWeek.Schedule.Count + 1];
                chartTable.Resize(chartRange);
                int countCol = 2;
                foreach (var item in dayOfTheWeek.Schedule)
                {
                    chartRange.Cells[1, countCol] = item.Duration.ToString();
                    chartRange.Cells[2, countCol] = item.Id_Profile.ToString();
                    countCol++;
                }
                */
                wDoc.SaveAs2($@"{Environment.CurrentDirectory}\{DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss")}.docx");
            }

            catch
            {
                MessageBox.Show($"Ошибка");
            }
            var process = Process.GetProcessesByName("Excel");
            foreach (var p in process)
            {
                if (!string.IsNullOrEmpty(p.ProcessName))
                {
                    try
                    {
                        p.Kill();
                    }
                    catch { }
                }
            }
        }

        private void btnOtchetCiti_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Word._Application wApp = new Word.Application();
                Word._Document wDoc = wApp.Documents.Add();
                wApp.Visible = true;
                wDoc.Activate();
                var ProductParagraph = wDoc.Content.Paragraphs.Add();
                //ProductParagraph.Range.Text = $"День недели:\t{dayOfTheWeek.Name}\n" + $"Статус:\t{shedule.Status}\n" + $"Время работы:\t{shedule.Duration}\n" + $"Цех:\t{shedule.Cabinet}\n";
                Word.Table wTable = wDoc.Tables.Add((Microsoft.Office.Interop.Word.Range)ProductParagraph.Range,
                cities.Count + 1, 1, Word.WdDefaultTableBehavior.wdWord9TableBehavior);
                wTable.Cell(1, 1).Range.Text = "Название города";
                wTable.Cell(1, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                int countRow = 2;
                foreach (var item in cities)
                {
                    wTable.Cell(countRow, 1).Range.Text = item.name.ToString();
                    wTable.Cell(countRow, 1).Range.Paragraphs.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    countRow++;
                }
                /*Word.Chart wChart;
                Word.InlineShape inlineShape;
                inlineShape = wDoc.InlineShapes.AddChart(Microsoft.Office.Core.XlChartType.xlColumnClustered, ProductParagraph.Range);
                wChart = inlineShape.Chart;

                dynamic chartWB = wChart.ChartData.Workbook;
                dynamic chartTable = chartWB.Sheets[1].ListObjects("Таблица1"); chartTable.DataBodyRange.ClearContents();
                dynamic chartRange = chartTable.Range.Resize[2, dayOfTheWeek.Schedule.Count + 1];
                chartTable.Resize(chartRange);
                int countCol = 2;
                foreach (var item in dayOfTheWeek.Schedule)
                {
                    chartRange.Cells[1, countCol] = item.Duration.ToString();
                    chartRange.Cells[2, countCol] = item.Id_Profile.ToString();
                    countCol++;
                }
                */
                wDoc.SaveAs2($@"{Environment.CurrentDirectory}\{DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss")}.docx");
            }

            catch
            {
                MessageBox.Show($"Ошибка");
            }
            var process = Process.GetProcessesByName("Excel");
            foreach (var p in process)
            {
                if (!string.IsNullOrEmpty(p.ProcessName))
                {
                    try
                    {
                        p.Kill();
                    }
                    catch { }
                }
            }
        }

        private void btnOtchetComp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Word._Application wApp = new Word.Application();
                Word._Document wDoc = wApp.Documents.Add();
                wApp.Visible = true;
                wDoc.Activate();
                var ProductParagraph = wDoc.Content.Paragraphs.Add();
                //ProductParagraph.Range.Text = $"День недели:\t{dayOfTheWeek.Name}\n" + $"Статус:\t{shedule.Status}\n" + $"Время работы:\t{shedule.Duration}\n" + $"Цех:\t{shedule.Cabinet}\n";
                Word.Table wTable = wDoc.Tables.Add((Microsoft.Office.Interop.Word.Range)ProductParagraph.Range,
                companys.Count + 1, 1, Word.WdDefaultTableBehavior.wdWord9TableBehavior);
                wTable.Cell(1, 1).Range.Text = "Название компании";
                wTable.Cell(1, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                int countRow = 2;
                foreach (var item in companys)
                {
                    wTable.Cell(countRow, 1).Range.Text = item.name.ToString();
                    wTable.Cell(countRow, 1).Range.Paragraphs.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    countRow++;
                }
                /*Word.Chart wChart;
                Word.InlineShape inlineShape;
                inlineShape = wDoc.InlineShapes.AddChart(Microsoft.Office.Core.XlChartType.xlColumnClustered, ProductParagraph.Range);
                wChart = inlineShape.Chart;

                dynamic chartWB = wChart.ChartData.Workbook;
                dynamic chartTable = chartWB.Sheets[1].ListObjects("Таблица1"); chartTable.DataBodyRange.ClearContents();
                dynamic chartRange = chartTable.Range.Resize[2, dayOfTheWeek.Schedule.Count + 1];
                chartTable.Resize(chartRange);
                int countCol = 2;
                foreach (var item in dayOfTheWeek.Schedule)
                {
                    chartRange.Cells[1, countCol] = item.Duration.ToString();
                    chartRange.Cells[2, countCol] = item.Id_Profile.ToString();
                    countCol++;
                }
                */
                wDoc.SaveAs2($@"{Environment.CurrentDirectory}\{DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss")}.docx");
            }

            catch
            {
                MessageBox.Show($"Ошибка");
            }
            var process = Process.GetProcessesByName("Excel");
            foreach (var p in process)
            {
                if (!string.IsNullOrEmpty(p.ProcessName))
                {
                    try
                    {
                        p.Kill();
                    }
                    catch { }
                }
            }
        }

        private void btnOtchetZ_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Word._Application wApp = new Word.Application();
                Word._Document wDoc = wApp.Documents.Add();
                wApp.Visible = true;
                wDoc.Activate();
                var ProductParagraph = wDoc.Content.Paragraphs.Add();
                //ProductParagraph.Range.Text = $"День недели:\t{dayOfTheWeek.Name}\n" + $"Статус:\t{shedule.Status}\n" + $"Время работы:\t{shedule.Duration}\n" + $"Цех:\t{shedule.Cabinet}\n";
                Word.Table wTable = wDoc.Tables.Add((Microsoft.Office.Interop.Word.Range)ProductParagraph.Range,
                senders.Count + 1, 4, Word.WdDefaultTableBehavior.wdWord9TableBehavior);
                wTable.Cell(1, 1).Range.Text = "Фамилия";
                wTable.Cell(1, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                wTable.Cell(1, 2).Range.Text = "Имя";
                wTable.Cell(1, 2).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                wTable.Cell(1, 3).Range.Text = "Отчество";
                wTable.Cell(1, 3).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                wTable.Cell(1, 4).Range.Text = "Компания";
                wTable.Cell(1, 4).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                int countRow = 2;
                foreach (var item in senders)
                {
                    wTable.Cell(countRow, 1).Range.Text = item.surname.ToString();
                    wTable.Cell(countRow, 1).Range.Paragraphs.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    wTable.Cell(countRow, 2).Range.Text = item.name.ToString();
                    wTable.Cell(countRow, 2).Range.Paragraphs.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    wTable.Cell(countRow, 3).Range.Text = item.middlename.ToString();
                    wTable.Cell(countRow, 3).Range.Paragraphs.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    wTable.Cell(countRow, 4).Range.Text = item.Company.name.ToString();
                    wTable.Cell(countRow, 4).Range.Paragraphs.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    countRow++;
                }
                /*Word.Chart wChart;
                Word.InlineShape inlineShape;
                inlineShape = wDoc.InlineShapes.AddChart(Microsoft.Office.Core.XlChartType.xlColumnClustered, ProductParagraph.Range);
                wChart = inlineShape.Chart;

                dynamic chartWB = wChart.ChartData.Workbook;
                dynamic chartTable = chartWB.Sheets[1].ListObjects("Таблица1"); chartTable.DataBodyRange.ClearContents();
                dynamic chartRange = chartTable.Range.Resize[2, dayOfTheWeek.Schedule.Count + 1];
                chartTable.Resize(chartRange);
                int countCol = 2;
                foreach (var item in dayOfTheWeek.Schedule)
                {
                    chartRange.Cells[1, countCol] = item.Duration.ToString();
                    chartRange.Cells[2, countCol] = item.Id_Profile.ToString();
                    countCol++;
                }
                */
                wDoc.SaveAs2($@"{Environment.CurrentDirectory}\{DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss")}.docx");
            }

            catch
            {
                MessageBox.Show($"Ошибка");
            }
            var process = Process.GetProcessesByName("Excel");
            foreach (var p in process)
            {
                if (!string.IsNullOrEmpty(p.ProcessName))
                {
                    try
                    {
                        p.Kill();
                    }
                    catch { }
                }
            }
        }

        private void dgOrders_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            GruziVeziEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            dgOrders.ItemsSource = GruziVeziEntities.GetContext().Orders.ToList();
            LoadDGOrders();
            LoadDGSenders();
            LoadDGCompany();
            LoadDGCities();
        }
    }
}
