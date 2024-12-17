using Popytka_3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace LB_3
{
    public partial class Form1 : Form
    {
        private Dictionary<string, string> companyData = new Dictionary<string, string>
        {
            { "DivFe", "54376273" },
            { "FoodEX", "65701191" },
            { "FD", "12345678" }
        };

        private List<string> orderLogs = new List<string>();
        private Dictionary<string, Dictionary<string, int>> companyOrders = new Dictionary<string, Dictionary<string, int>>();
        private double totalIncome = 0;
        public delegate void OrderReceivedEventHandler(object sender, OrderEventArgs e);
        public event OrderReceivedEventHandler OrderReceived;

        public Form1()
        {
            InitializeComponent();
            this.Text = "FooDelivery";

            // Підписуємося на подію
            this.OrderReceived += OnOrderReceived;

            LoadCompanies();
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            InitializeOrderStorage();
            LoadUsersFromFile();
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;

        }


        private void InitializeOrderStorage()
        {
            foreach (var company in companyData.Keys)
            {
                companyOrders[company] = new Dictionary<string, int>();
            }
        }
        private void RaiseOrderReceived(string company, string orderType, string userName)
        {
            OrderReceived?.Invoke(this, new OrderEventArgs
            {
                Company = company,
                OrderType = orderType,
                Time = DateTime.Now,
                UserName = userName // Передаем имя пользователя
            });
        }


        // Крок 4: Обробник події
        private void OnOrderReceived(object sender, OrderEventArgs e)
        {
            MessageBox.Show(
                $"Отримано замовлення!\nКомпанія: {e.Company}\nТип: {e.OrderType}\nЧас: {e.Time}\nКористувач: {e.UserName}",
                "Подія замовлення",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information

            );
            string logEntry = $"{e.Time}: Подія замовлення - Компанія: {e.Company}, Тип: {e.OrderType}, Користувач: {e.UserName}";
            orderLogs.Add(logEntry);

        }


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
            string currentMonth = DateTime.Now.ToString("yyyy-MM");

            if (!companyOrders[selectedCompany].ContainsKey(currentMonth))
                companyOrders[selectedCompany][currentMonth] = 0;

            companyOrders[selectedCompany][currentMonth]++;

            string logEntry = $"{DateTime.Now}: Компания '{selectedCompany}' - {orderType}";
            orderLogs.Add(logEntry);
        }

        private List<User> registeredUsers = new List<User>();

        public class User
        {
            public string Name { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }
        }

        public class OrderEventArgs : EventArgs
        {
            public string Company { get; set; }
            public string OrderType { get; set; }
            public DateTime Time { get; set; }
            public string UserName { get; set; } // Новое свойство для имени пользователя
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCompany = comboBox1.SelectedItem.ToString();

            if (companyData.ContainsKey(selectedCompany))
            {
                listBox1.Items.Clear();
                listBox1.Items.Add(companyData[selectedCompany]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selectedUser = comboBox2.SelectedItem?.ToString(); // Получаем имя пользователя
            if (string.IsNullOrEmpty(selectedUser))
            {
                MessageBox.Show("Выберите пользователя перед оформлением заказа.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LogOrder("Заказ через кнопку 1");
            RaiseOrderReceived(comboBox1.SelectedItem.ToString(), "Заказ через кнопку 1", selectedUser);

            Button_3 form = new Button_3();
            if (form.ShowDialog() == DialogResult.OK)
            {
                totalIncome += form.GetFinalPrice();
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string selectedUser = comboBox2.SelectedItem?.ToString(); // Получаем имя пользователя
            if (string.IsNullOrEmpty(selectedUser))
            {
                MessageBox.Show("Выберите пользователя перед оформлением заказа.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LogOrder("Заказ через кнопку 1");
            RaiseOrderReceived(comboBox1.SelectedItem.ToString(), "Заказ через кнопку 1", selectedUser);

            Button_3 form = new Button_3();
            if (form.ShowDialog() == DialogResult.OK)
            {
                totalIncome += form.GetFinalPrice();
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            string selectedUser = comboBox2.SelectedItem?.ToString(); // Получаем имя пользователя
            if (string.IsNullOrEmpty(selectedUser))
            {
                MessageBox.Show("Выберите пользователя перед оформлением заказа.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LogOrder("Заказ через кнопку 1");
            RaiseOrderReceived(comboBox1.SelectedItem.ToString(), "Заказ через кнопку 1", selectedUser);

            Button_3 form = new Button_3();
            if (form.ShowDialog() == DialogResult.OK)
            {
                totalIncome += form.GetFinalPrice();
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            string selectedUser = comboBox2.SelectedItem?.ToString(); // Получаем имя пользователя
            if (string.IsNullOrEmpty(selectedUser))
            {
                MessageBox.Show("Выберите пользователя перед оформлением заказа.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LogOrder("Заказ через кнопку 1");
            RaiseOrderReceived(comboBox1.SelectedItem.ToString(), "Заказ через кнопку 1", selectedUser);

            Button_3 form = new Button_3();
            if (form.ShowDialog() == DialogResult.OK)
            {
                totalIncome += form.GetFinalPrice();
            }
        }


        private void button6_Click(object sender, EventArgs e)
        {
            string selectedCompany = comboBox1.SelectedItem.ToString();

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

        private void button7_Click(object sender, EventArgs e)
        {
            string logs = "Логи заказов:\n\n" + string.Join("\n", orderLogs);
            MessageBox.Show(logs, "Логи операций", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show($"Общий доход: {totalIncome:F2} грн", "Доходы", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SaveLogsToFile()
        {
            string filePath = "OrderLogs.txt";

            try
            {
                File.WriteAllLines(filePath, orderLogs);
                MessageBox.Show($"Логи успешно сохранены в файл: {filePath}", "Сохранение логов", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении логов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveUsersToFile()
        {
            string filePath = "RegisteredUsers.txt";

            try
            {
                // Преобразование данных пользователей в строки для записи
                List<string> userLines = registeredUsers.Select(user => $"{user.Name};{user.Address};{user.Phone}").ToList();

                // Запись в файл
                File.WriteAllLines(filePath, userLines);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных пользователей: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadUsersToComboBox2()
        {
            comboBox2.Items.Clear();
            foreach (var user in registeredUsers)
            {
                comboBox2.Items.Add(user.Name);
            }

            if (comboBox2.Items.Count > 0)
                comboBox2.SelectedIndex = 0; // Устанавливаем первый элемент по умолчанию
        }

        private void LoadUsersFromFile()
        {
            string filePath = "RegisteredUsers.txt";

            if (File.Exists(filePath))
            {
                try
                {
                    string[] userLines = File.ReadAllLines(filePath);

                    foreach (string line in userLines)
                    {
                        string[] parts = line.Split(';');
                        if (parts.Length == 3)
                        {
                            User user = new User
                            {
                                Name = parts[0],
                                Address = parts[1],
                                Phone = parts[2]
                            };
                            registeredUsers.Add(user);
                        }
                    }

                    
                    LoadUsersToComboBox2(); // Обновляем comboBox2
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке данных пользователей: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            SaveLogsToFile();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // Запрос данных у пользователя
            string userName = Microsoft.VisualBasic.Interaction.InputBox("Введите имя пользователя:", "Регистрация пользователя");
            if (string.IsNullOrWhiteSpace(userName))
            {
                MessageBox.Show("Имя пользователя не может быть пустым!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string userAddress = Microsoft.VisualBasic.Interaction.InputBox("Введите адрес пользователя:", "Регистрация пользователя");

            string userPhone = Microsoft.VisualBasic.Interaction.InputBox("Введите телефон пользователя (+380XXXXXXXXX):", "Регистрация пользователя");

            // Проверка номера телефона
            if (userPhone.Length != 13 || !userPhone.StartsWith("+380"))
            {
                MessageBox.Show("Номер телефона должен быть в формате +380XXXXXXXXX и содержать ровно 13 символов.",
                                "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Сохранение данных
            User newUser = new User
            {
                Name = userName,
                Address = userAddress,
                Phone = userPhone
            };

            registeredUsers.Add(newUser);

            // Сохранение в файл
            SaveUsersToFile();
            LoadUsersToComboBox2();


            // Уведомление о регистрации
            MessageBox.Show($"Пользователь {userName} успешно зарегистрирован!", "Регистрация завершена", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        private void button9_Click(object sender, EventArgs e)
        {
            if (registeredUsers.Count == 0)
            {
                MessageBox.Show("Нет зарегистрированных пользователей.", "Пользователи", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string userList = "Зарегистрированные пользователи:\n\n";
            foreach (var user in registeredUsers)
            {
                userList += $"- {user.Name}, Адрес: {user.Address}, Телефон: {user.Phone}\n";
            }

            MessageBox.Show(userList, "Пользователи", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedUserName = comboBox2.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(selectedUserName))
            {
                var selectedUser = registeredUsers.FirstOrDefault(u => u.Name == selectedUserName);
            }
        }

    }
}
