using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using HtmlAgilityPack;
using System.Drawing;

namespace Excel
{
    public partial class Form1 : Form
    {

        double numericValue;
        public Form1()
        {
            InitializeComponent();
            // Set the DrawMode to OwnerDrawFixed
            listBox4.DrawMode = DrawMode.OwnerDrawFixed;

            // Attach the DrawItem event
            listBox4.DrawItem += ListBox_DrawItem;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filePath = @"C:\Users\Anand\Downloads\" + listBox3.SelectedItem; // Replace with your file path
            int column1Index = 1; // Index of the first column (0-based, e.g., 0 for Column A)
            int column2Index = 3; // Index of the second column (0-based, e.g., 1 for Column B)

            // Configure CsvReader
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true, // Assuming the first row is a header
            };

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                // Read CSV into records
                var records = new List<string[]>();
                while (csv.Read())
                {
                    var row = new List<string>();
                    for (var i = 0; csv.TryGetField(i, out string field); i++)
                    {
                        row.Add(field);
                    }
                    records.Add(row.ToArray());
                }

                Console.WriteLine("Rows with matching values:");
                listBox1.Items.Add("Rows with matching values:");

                for (int i = 1; i < records.Count; i++) // Start from 1, assuming the first row is the header
                {
                    var row = records[i];
                    if (row[column1Index] == row[column2Index])
                    {
                        Console.WriteLine($"Row {i + 1}: {string.Join(", ", row)}");
                        listBox1.Items.Add($"Row {i + 1}: {string.Join(", ", row)}");
                        Console.WriteLine($"Column Header: {records[i][0]}");
                        listBox2.Items.Add(records[i][0]);
                        listBox4.Items.Add(records[i][0] + "   " + records[i][8]);

                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string url = "https://in.tradingview.com/chart/?symbol=" + listBox2.SelectedItem; // Replace with your desired URL

            try
            {
                // Specify the path to Chrome's executable file
                string chromePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe";

                // Start Chrome with the URL
                Process.Start(chromePath, url);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //https://www.nseindia.com/api/equity-stockIndices?csv=true&index=NIFTY%2050&selectValFormat=crores

            string url = textBox1.Text; // Replace with your desired URL

            try
            {
                // Specify the path to Chrome's executable file
                string chromePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe";

                // Start Chrome with the URL
                Process.Start(chromePath, url);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string downloadsFolder = @"C:\Users\Anand\Downloads"; // Replace with your Downloads folder path
            DateTime today = DateTime.Today; // Gets today's date with time set to 00:00:00

            try
            {
                // Get all files in the Downloads directory
                string[] files = Directory.GetFiles(downloadsFolder);

                Console.WriteLine("Files downloaded today:");
                listBox3.Items.Add("Files downloaded today:");
                foreach (string file in files)
                {
                    // Get file information
                    FileInfo fileInfo = new FileInfo(file);

                    // Check if the file was created or modified today
                    if (fileInfo.CreationTime.Date == today || fileInfo.LastWriteTime.Date == today)
                    {
                        Console.WriteLine(fileInfo.Name);
                        listBox3.Items.Add(fileInfo.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            listBox4.Items.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string url = "https://www.google.com/search?q=" + listBox2.SelectedItem; // Replace with your desired URL
            string url1 = "https://www.screener.in/company/" + listBox2.SelectedItem;

            try
            {
                // Specify the path to Chrome's executable file
                string chromePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe";

                // Start Chrome with the URL
                Process.Start(chromePath, url);
                Process.Start(chromePath, url1);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string stockSymbol = textBox2.Text; // Get the stock symbol from TextBox
            if (!string.IsNullOrEmpty(stockSymbol))
            {
                string stockPrice = ScrapeGoogleFinance(stockSymbol); // Scrape stock price
                string currencyString = stockPrice;
                try
                {
                    // Remove the currency symbol and parse as a double
                    var cultureInfo = new CultureInfo("en-IN");
                    double numericValue = double.Parse(currencyString, NumberStyles.Currency, cultureInfo);

                    // Print the numeric value
                    Console.WriteLine(numericValue); // Output: 1834.00
                    //MessageBox.Show("NUMERIC VALUE  1 : " + numericValue);
                    double number1 = numericValue;
                    double number2 = 50000;
                    double number3 = 100000;
                    double number4 = Convert.ToDouble(textBox4.Text);
                    double result = number2 / number1;
                    label2.Text = "For 50 K :" + result.ToString();
                    double result1 = number3 / number1;
                    label3.Text = "For 1L  :" + result1.ToString();
                    double result4 = number4 / number1;
                    // MessageBox.Show(number4.ToString());
                    label6.Text = "For " + number4 + ":" + result4.ToString();

                    // percent

                    textBox3.Text = numericValue.ToString();


                    //
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid currency format.");
                }
                if (!string.IsNullOrEmpty(stockPrice))
                {
                    label1.Text = $"The current price of {stockSymbol} is {stockPrice}";
                    //MessageBox.Show(stockPrice);

                }
                else
                {
                    label1.Text = "Stock price not found. Please check the symbol.";
                }
            }
            else
            {
                label1.Text = "Please enter a stock symbol.";
            }
        }

        private string ScrapeGoogleFinance(string symbol)
        {
            try
            {
                string url = $"https://www.google.com/finance/quote/{symbol}";

                // MessageBox.Show("url: is " + url);
                HtmlWeb web = new HtmlWeb();
                var doc = web.Load(url);

                // Use XPath to locate the stock price
                var priceNode = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'YMlKec fxKbKc')]");
                return priceNode?.InnerText; // Return the stock price or null if not found
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Text = listBox2.SelectedItem.ToString() + ":NSE";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

            try
            {
                // Get the value from the TextBox
                string input = textBox3.Text;

                // Parse the value to a double
                double value = double.Parse(input);

                // Calculate 1% of the value
                double onePercent = value * 0.01 + value;
                double twoPercent = value * 0.02 + value;

                // Display the result
                label4.Text = $"1% of {value} is {onePercent}";
                label5.Text = $"2% of {value} is {twoPercent}";
            }
            catch (FormatException)
            {
                label5.Text = "Invalid input. Please enter a numeric value.";
            }
            catch (Exception ex)
            {
                label4.Text = $"An error occurred: {ex.Message}";
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = listBox1.SelectedItem.ToString();
            string[] parts = selectedItem.Split(',');

            // Check if there are at least 9 items
            if (parts.Length >= 9)
            {
                // Display the 9th item
                MessageBox.Show($"The 9th item is: {parts[8]}", "9th Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Text = selectedItem;
            }
            else
            {
                MessageBox.Show("The selected item does not contain at least 9 items.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (listBox4.SelectedIndex >= 0 && listBox2.SelectedIndex < listBox4.Items.Count)
            {
                listBox2.SelectedIndex = listBox4.SelectedIndex;
                button7_Click(sender, e);

                if (checkBox1.Checked == true)
                {
                    button2_Click(sender, e);
                }
                if (checkBox2.Checked == true)
                {
                    button6_Click(sender, e);
                }


            }
        }
        //for color
        private void ListBox_DrawItem(object sender, DrawItemEventArgs e)
        {

            if (e.Index < 0) return; // Ensure valid index

            // Get the item text from ListBox4
            string itemText = listBox4.Items[e.Index].ToString();

            // Extract the numeric value from the item text
            double number = ExtractNumber(itemText);

            // Determine text color based on the value
            Brush textBrush;

            if (number > 2)
            {
                textBrush = Brushes.Red; // Red for numbers greater than 2
            }
            else if (number > 1)
            {
                textBrush = Brushes.Orange; // Orange for numbers between 1 and 2
            }
            else if (number > 0)
            {
                textBrush = Brushes.Blue; // Blue for numbers between 0 and 1
            }
            else
            {
                textBrush = Brushes.Yellow; // Yellow for numbers less than 0
            }

            // Draw the background
            e.DrawBackground();

            // Draw the item text
            e.Graphics.DrawString(itemText, e.Font, textBrush, e.Bounds);

            // Draw the focus rectangle if selected
            e.DrawFocusRectangle();

        }

        private double ExtractNumber(string itemText)
        {
            // Split the string by space (adjust based on your item format)
            string[] parts = itemText.Split(' ');

            // Ensure the array has at least one element
            if (parts.Length > 1 && double.TryParse(parts[parts.Length - 1], out double result))
            {
                return result;
            }
            return 0; // Default if parsing fails
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox4.Items.Count; i++)
            {
                string itemText = listBox4.Items[i].ToString();
                double number = ExtractNumber(itemText);

                // Check if the number is between 0 and 1 (for Blue)
                if (number > 0 && number <= 1)
                {
                    listBox4.SetSelected(i, true); // Select the item
                    button2_Click(sender, e);
                }
                else
                {
                    listBox4.SetSelected(i, false); // Deselect the item if it's not blue
                    //button2_Click(sender, e);
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox4.Items.Count; i++)
            {
                string itemText = listBox4.Items[i].ToString();
                double number = ExtractNumber(itemText);

                // Check if the number is between 0 and 1 (for Blue)
                if (number > 2 && number <= 10)
                {
                    listBox4.SetSelected(i, true); // Select the item
                    button2_Click(sender, e);
                }
                else
                {
                    listBox4.SetSelected(i, false); // Deselect the item if it's not blue
                    //button2_Click(sender, e);
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox4.Items.Count; i++)
            {
                string itemText = listBox4.Items[i].ToString();
                double number = ExtractNumber(itemText);

                // Check if the number is between 0 and 1 (for Blue)
                if (number > 1 && number <= 2)
                {
                    listBox4.SetSelected(i, true); // Select the item
                    button2_Click(sender, e);
                }
                else
                {
                    listBox4.SetSelected(i, false); // Deselect the item if it's not blue
                    //button2_Click(sender, e);
                }
            }
        }
    }
}