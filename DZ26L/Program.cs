using System;
using System.Collections.Generic;

// Класс сервиса доставки пиццы
class PizzaDeliveryService
{
    // Событие при заказе пиццы
    public event EventHandler<Tuple<string, List<string>>> PizzaOrdered;

    // Метод для размещения заказа
    public void PlaceOrder(string customerName, List<string> pizzas)
    {
        OnPizzaOrdered(customerName, pizzas);
    }

    // Метод для вызова события заказа пиццы
    protected void OnPizzaOrdered(string customerName, List<string> pizzas)
    {
        PizzaOrdered?.Invoke(this, Tuple.Create(customerName, pizzas));

        // Реализация простой системы лояльности
        if (pizzas.Count / 2 > 0)
        {
            string loyaltyMessage = $"Поздравляем, {customerName}! Вы получили {pizzas.Count / 2} бесплатных маргарит!";
            Console.WriteLine(loyaltyMessage);
        }
    }

    // Метод-обработчик события PizzaOrdered
    public void HandlePizzaOrdered(object sender, Tuple<string, List<string>> orderInfo)
    {
        string customerName = orderInfo.Item1;
        List<string> pizzas = orderInfo.Item2;

        Console.WriteLine($"Заказаны следующие пиццы для {customerName}:");
        foreach (var pizza in pizzas)
        {
            Console.WriteLine(pizza);
        }
    }

    // Конструктор класса
    public PizzaDeliveryService()
    {
        // Привязываем метод HandlePizzaOrdered к событию PizzaOrdered
        PizzaOrdered += HandlePizzaOrdered;
    }
}

class Program
{
    static void Main(string[] args)
    {
        PizzaDeliveryService deliveryService = new PizzaDeliveryService(); // Создание сервиса доставки пиццы

        // Симуляция заказов пиццы
        List<string> order1 = new List<string> { "Сырная" };
        List<string> order2 = new List<string> { "Маргарита", "Пепперони", };
        List<string> order3 = new List<string> { "Сырная", "Маргарита", "Маргарита" };
        List<string> order4 = new List<string> { "Сырная", "Маргарита", "Маргарита", "Гавайская" };
        deliveryService.PlaceOrder("Алиса", order1);
        Console.WriteLine();
        deliveryService.PlaceOrder("Маша", order2);
        Console.WriteLine();
        deliveryService.PlaceOrder("Петя", order3);
        Console.WriteLine();
        deliveryService.PlaceOrder("И-и-игорь", order4);
    }
}
