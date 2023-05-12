namespace project.Models
{
    public static class Listino
    {
        private static List<Pizza> pizze = new List<Pizza>()
        {
            new Pizza("Margherita", "Pomodoro, mozzarella", "Margherita.jpg", 5.50),
            new Pizza("Marinara", "Pomodoro, olio, origano", "Marinara.jpg", 5.00),
            new Pizza("Calzone", "Pomodoro, mozzarella, prosciutto", "Calzone.webp", 7.00),
            new Pizza("Americana", "Pomodoro, mozzarella, wurstel, patatine", "Americana.webp", 7.50),
            new Pizza("Salame e taleggio", "Pomodoro, mozzarella, salame, taleggio", "Salame.jpg", 5.00),
            new Pizza("Diavola", "Pomodoro, mozzarella, salamino piccante", "Diavola.jpg", 7.00),
            new Pizza("Capricciosa", "Pomodoro, mozzarella, prosciutto, funghi, carciofi, olive, capperi", "Capricciosa.jpg", 8.50),
            new Pizza("Quattro formaggi", "Pomodoro, mozzarella, zola, taleggio, grana", "Quattro.jpg", 8.50),
            new Pizza("Prosciutto e funghi", "Pomodoro, mozzarella, prosciutto, funghi", "PF.webp", 7.00),
            new Pizza("Speciale", "Pomodoro, mozzarella, uovo, prosciutto, funghi, salamino piccante", "Speciale.jpg", 9.50),
        };
        public static List<Pizza> Pizze { get { return pizze; } }
    }
}
