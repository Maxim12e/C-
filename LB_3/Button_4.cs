﻿using System;
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
    public partial class Button_4 : Form
    {
        // Словарь с блюдами и ценами
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

        public Button_4()
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

        public double GetFinalPrice()
        {
            double totalPrice = 0;
            const double courierRate = 0.08; // Скидка 8%

            foreach (string selectedItem in listBox1.SelectedItems)
            {
                string dishName = selectedItem.Split('-')[0].Trim();
                if (dishes.TryGetValue(dishName, out var details))
                {
                    totalPrice += details.Price;
                }
            }

            double courierFee = totalPrice * courierRate;
            return totalPrice - courierFee; // Итоговая цена
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int totalPrice = 0;
            int totalCookingTime = 0; // Суммарное время готовки
            const double courierRate = 0.08; // Надбавка за услуги курьера (5%)

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

            double courierFee = totalPrice * courierRate; // Размер надбавки за курьера
            double finalPrice = totalPrice - courierFee; // Итоговая цена с надбавкой

            // Обновляем текст меток
            totalPriceLabel.Text = $"Цена: {totalPrice} грн\n\nБлаготворительная скидка : {courierFee:F2} грн\n\nСумма к оплате: {finalPrice:F2} грн";
            timeLabel.Text = $"Общее время готовки: {totalCookingTime} минут";
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                MessageBox.Show(
                    "Спасибо за заказ!\nАдресс: Улица Вилка 11в\nВремя доставки: 3 часа\nСтатус: Оплачено",
                    "СТАТУС",
                    MessageBoxButtons.OK
                );
            else
            {
                MessageBox.Show(
                    "Спасибо за заказ!\nВремя доставки: 3 часа\nОплата онлайн не произведена",
                    "СТАТУС",
                    MessageBoxButtons.OK
                );
            }
            this.DialogResult = DialogResult.OK; // Закрываем форму с результатом
        }
    }
}
