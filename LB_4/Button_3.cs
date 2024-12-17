using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Popytka_3
{
    public partial class Button_3 : Form
    {
        // Словарь с блюдами (название -> цена и время готовки)
        private readonly Dictionary<string, (int Price, int CookingTime)> dishes = new Dictionary<string, (int, int)>
        {
            { "Пицца", (200, 20) },
            { "Борщ", (90, 10) },
            { "Суши", (300, 25) },
            { "Паста", (150, 35) },
            { "Салат", (50, 15) }
        };

        // Словарь с ресторанами (город -> название и улица)
        private readonly Dictionary<string, (string Restaurant, string Street)> restaurants = new Dictionary<string, (string, string)>
        {
            { "Харьков", ("Ресторан Харьков", "улица Состоятельна 11а") },
            { "Париж", ("Ресторан Париж", "улица Состоятельна 12б") },
            { "Вишня", ("Ресторан Вишня", "улица Состоятельна 13в") }
        };

        // Константы для скидок
        private const double DiscountRate = 0.08; // Скидка 8%
        private const double PickupDiscountRate = 0.05; // Скидка за самовывоз 5%

        // Метки для отображения данных
        private Label totalPriceLabel;
        private Label orderTimeLabel;
        private Label cookingTimeLabel;

        public Button_3()
        {
            InitializeComponent();
            InitializeCustomControls();
            PopulateDishesList();
            PopulateRestaurantsList();
        }

        private void InitializeCustomControls()
        {
            // Метка для общей стоимости
            totalPriceLabel = new Label
            {
                Text = "Общая стоимость: 0 грн",
                AutoSize = true,
                Location = new System.Drawing.Point(10, 250)
            };
            this.Controls.Add(totalPriceLabel);

            // Метка для общего времени готовки
            cookingTimeLabel = new Label
            {
                Text = "Общее время готовки: 0 минут",
                AutoSize = true,
                Location = new System.Drawing.Point(10, 330)
            };
            this.Controls.Add(cookingTimeLabel);

            // Метка для времени заказа
            orderTimeLabel = new Label
            {
                Text = $"Время заказа: {DateTime.Now:dd.MM.yyyy HH:mm:ss}",
                AutoSize = true,
                Location = new System.Drawing.Point(10, 230)
            };
            this.Controls.Add(orderTimeLabel);

            // Настройка событий
            listBox1.SelectionMode = SelectionMode.MultiExtended;
            listBox2.SelectionMode = SelectionMode.One;

            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
        }

        private void PopulateDishesList()
        {
            foreach (var dish in dishes)
            {
                listBox1.Items.Add($"{dish.Key} - {dish.Value.Price} грн, {dish.Value.CookingTime} минут");
            }
        }

        private void PopulateRestaurantsList()
        {
            foreach (var restaurant in restaurants)
            {
                listBox2.Items.Add($"{restaurant.Key} - {restaurant.Value.Street}");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Подсчет общей стоимости и времени приготовления
            int totalPrice = 0;
            int totalCookingTime = 0;

            foreach (string selectedItem in listBox1.SelectedItems)
            {
                string dishName = selectedItem.Split('-')[0].Trim();
                if (dishes.TryGetValue(dishName, out var details))
                {
                    totalPrice += details.Price;
                    totalCookingTime += details.CookingTime;
                }
            }

            // Рассчитываем скидки
            double discount = totalPrice * DiscountRate;
            double pickupDiscount = totalPrice * PickupDiscountRate;
            double finalPrice = totalPrice - (discount + pickupDiscount);

            // Обновляем метки
            totalPriceLabel.Text = $"Цена: {totalPrice} грн\n" +
                                   $"Скидка 8%: {discount:F2} грн\n" +
                                   $"Скидка за самовывоз 5%: {pickupDiscount:F2} грн\n" +
                                   $"Сумма к оплате: {finalPrice:F2} грн";

            cookingTimeLabel.Text = $"Общее время готовки: {totalCookingTime} минут";
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                MessageBox.Show("Спасибо за заказ! Оплачено.", "СТАТУС", MessageBoxButtons.OK);
            else
                MessageBox.Show("Спасибо за заказ! Оплата не произведена.", "СТАТУС", MessageBoxButtons.OK);

            this.DialogResult = DialogResult.OK; // Закрываем форму с результатом
        }

        public double GetFinalPrice()
        {
            double totalPrice = 0;

            foreach (string selectedItem in listBox1.SelectedItems)
            {
                string dishName = selectedItem.Split('-')[0].Trim();
                if (dishes.TryGetValue(dishName, out var details))
                {
                    totalPrice += details.Price;
                }
            }

            double discount = totalPrice * DiscountRate;
            double pickupDiscount = totalPrice * PickupDiscountRate;
            return totalPrice - (discount + pickupDiscount);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
