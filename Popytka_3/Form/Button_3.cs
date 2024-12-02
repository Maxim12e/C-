using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Popytka_3
{
    public partial class Button_3 : Form
    {

        private Dictionary<string, (int Price, int CookingTime)> dishes = new Dictionary<string, (int, int)>
        {
            { "Пицца", (200, 20) },
            { "Борщ", (90, 10) },
            { "Суши", (300, 25) },
            { "Паста", (150, 35) },
            { "Салат", (50, 15) }
        };

        private Dictionary<string, (string Restaurant, string Street)> Restaurant = new Dictionary<string, (string, string)>
        {
            { "Харьков", ("Ресторан Харьков", "улица Состоятельна 11а") },
            { "Париж", ("Ресторан Париж", "улица Состоятельна 12б") },
            { "Вишня", ("Ресторан Вишня", "улица Состоятельна 13в") }
        };

        private Label totalPriceLabel; // Метка для отображения суммы
        private Label orderTimeLabel; // Метка для отображения времени заказа
        private Label timeLabel;      // Метка для отображения общего времени готовки
        public Button_3()
        {
            InitializeComponent();
            // Инициализируем метку для отображения суммы
            totalPriceLabel = new Label
            {
                Text = "Общая стоимость: 0 грн",
                AutoSize = true,
                Location = new System.Drawing.Point(10, 250) // Расположение метки
            };
            this.Controls.Add(totalPriceLabel);

            // Метка для общего времени приготовления
            timeLabel = new Label
            {
                Text = "Общее время готовки: 0 минут",
                AutoSize = true,
                Location = new System.Drawing.Point(10, 330) // Расположение метки
            };
            this.Controls.Add(timeLabel);

            // Метка для отображения времени заказа
            orderTimeLabel = new Label
            {
                Text = $"Время заказа: {DateTime.Now:dd.MM.yyyy HH:mm:ss}",
                AutoSize = true,
                Location = new System.Drawing.Point(10, 230) // Расположение метки
            };
            this.Controls.Add(orderTimeLabel);

            // Настройка ListBox для выбора блюд
            listBox1.SelectionMode = SelectionMode.MultiExtended; // Разрешаем выбор нескольких элементов

            // Добавляем блюда с ценами и временем приготовления в ListBox
            foreach (var dish in dishes)
            {
                listBox1.Items.Add($"{dish.Key} - {dish.Value.Price} грн, {dish.Value.CookingTime} минут");
            }

            // Настройка ListBox для выбора ресторанов
            listBox2.SelectionMode = SelectionMode.One; // Для ресторанов оставляем выбор одного элемента
            foreach (var rest in Restaurant)
            {
                listBox2.Items.Add($"{rest.Key} - {rest.Value.Street}");
            }

            // Привязываем обработчик события
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int totalPrice = 0;
            int totalCookingTime = 0; // Суммарное время готовки
            const double discountRate = 0.08; // Ставка скидки (5%)
            const double discountRate_2 = 0.05; // Ставка скидки (5%)

            // Обрабатываем все выбранные элементы
            foreach (string selectedItem in listBox1.SelectedItems)
            {
                // Извлекаем название блюда (до первого '-')
                string dishName = selectedItem.Split('-')[0].Trim();

                // Получаем данные из словаря
                if (dishes.TryGetValue(dishName, out var details))
                {
                    totalPrice += details.Price;
                    totalCookingTime += details.CookingTime; // Суммируем время приготовления
                }
            }

            // Рассчитываем скидку и итоговую сумму
            double discountAmount = totalPrice * discountRate; // Размер скидки
            double discountAmount_2 = totalPrice * discountRate_2; // Размер скидки
            double finalPrice = totalPrice - (discountAmount + discountAmount_2); // Итоговая цена со скидкой


            // Обновляем текст меток
            totalPriceLabel.Text = $"Цена: {totalPrice} грн\nСкидка 8%: {discountAmount:F2} грн\nСкидка за самовывоз 5%: {discountAmount_2:F2} грн\n" +
                $"Сумма к оплате: {finalPrice:F2} грн";

            timeLabel.Text = $"Общее время готовки: {totalCookingTime} минут";
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) // Проверяем, установлен ли чекбокс
            {
                MessageBox.Show(
                    "Спасибо за заказ!\nСтатус: Оплачено",
                    "СТАТУС",
                    MessageBoxButtons.OK
                );
            }
            else
            {
                MessageBox.Show(
                    "Спасибо за заказ!\nОплата онлайн не произведена",
                    "СТАТУС",
                    MessageBoxButtons.OK
                );
            }
        }
    }
}
