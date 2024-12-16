using Popytka_3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LB_3
{
    public partial class Form1 : Form
    {
        // Словарь для хранения компаний и их кодов ЄДРПОУ
        private Dictionary<string, string> companyData = new Dictionary<string, string>
        {
            { "DivFe", "54376273" },
            { "FoodEX", "65701191" },
            { "FD", "12345678" }
        };

        // Хранилище логов и заказов
        private List<string> orderLogs = new List<string>(); // Логи заказов
        private Dictionary<string, Dictionary<string, int>> companyOrders = new Dictionary<string, Dictionary<string, int>>();
        private double totalIncome = 0; // Общий доход

        public Form1()
        {
            InitializeComponent();
            this.Text = "FooDelivery"; // Название формы
            LoadCompanies();
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            InitializeOrderStorage();
        }

        private void InitializeOrderStorage()
        {
            foreach (var company in companyData.Keys)
            {
                companyOrders[company] = new Dictionary<string, int>();
            }
        }

        // Метод для заполнения ComboBox названиями компаний
        private void LoadCompanies()
        {
            comboBox1.Items.Clear();
            foreach (var company in companyData.Keys)
            {
                comboBox1.Items.Add(company);
            }
            comboBox1.SelectedIndex = 0;
        }
        private void LogOrder(string orderType)
        {
            string selectedCompany = comboBox1.SelectedItem.ToString();
            string currentMonth = DateTime.Now.ToString("yyyy-MM"); // Текущий месяц

            // Обновляем счетчик заказов для компании
            if (!companyOrders[selectedCompany].ContainsKey(currentMonth))
                companyOrders[selectedCompany][currentMonth] = 0;

            companyOrders[selectedCompany][currentMonth]++;

            // Логируем операцию
            string logEntry = $"{DateTime.Now}: Компания '{selectedCompany}' - {orderType}";
            orderLogs.Add(logEntry);
        }

        // Обработчик изменения выбранного элемента в ComboBox
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCompany = comboBox1.SelectedItem.ToString();

            if (companyData.ContainsKey(selectedCompany))
            {
                listBox1.Items.Clear();
                listBox1.Items.Add(companyData[selectedCompany]);
            }
        }

        // Обработчик кнопки button2: Звичайне замовлення, самовивіз
        // Обработчик кнопки button2: Звичайне замовлення, самовивіз
        // Обработчик кнопки button2: Звичайне замовлення, самовивіз
        private void button2_Click(object sender, EventArgs e)
        {
            LogOrder("Заказ через кнопку 1");

            // Создаем форму для заказа и вызываем ее
            Button_3 form = new Button_3();
            if (form.ShowDialog() == DialogResult.OK)
            {
                totalIncome += form.GetFinalPrice(); // Добавляем доход от заказа
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            LogOrder("Заказ через кнопку 2");

            // Создаем форму для заказа и вызываем ее
            Button_3 form = new Button_3();
            if (form.ShowDialog() == DialogResult.OK)
            {
                totalIncome += form.GetFinalPrice(); // Добавляем доход от заказа
            }
        }



        private void button4_Click(object sender, EventArgs e)
        {
            LogOrder("Акційне замовлення, самовивіз"); // Логируем заказ
            Button_3 form = new Button_3();
            if (form.ShowDialog() == DialogResult.OK)
            {
                totalIncome += form.GetFinalPrice(); // Добавляем доход
            }
        }



        private void button5_Click(object sender, EventArgs e)
        {
            LogOrder("Благодійне замовлення");
            Button_4 form = new Button_4();
            if (form.ShowDialog() == DialogResult.OK)
            {
                totalIncome += form.GetFinalPrice(); // Добавляем доход
            }
        }



        // Обработчик для кнопки Заказы
        private void button6_Click(object sender, EventArgs e)
        {
            string selectedCompany = comboBox1.SelectedItem.ToString();

            // Проверяем наличие заказов для выбранной компании
            if (companyOrders.ContainsKey(selectedCompany) && companyOrders[selectedCompany].Any())
            {
                string report = $"Количество заказов для компании '{selectedCompany}':\n\n";
                foreach (var month in companyOrders[selectedCompany])
                {
                    report += $"Месяц {month.Key}: {month.Value} заказов\n";
                }
                MessageBox.Show(report, "Отчет по заказам", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Для компании '{selectedCompany}' заказов пока нет.", "Отчет по заказам", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        // Обработчик для кнопки Логи
        private void button7_Click(object sender, EventArgs e)
        {
            string logs = "Логи заказов:\n\n" + string.Join("\n", orderLogs);
            MessageBox.Show(logs, "Логи операций", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show($"Общий доход: {totalIncome:F2} грн", "Доходы", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
