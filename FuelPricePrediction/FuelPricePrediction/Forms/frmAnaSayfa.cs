using Microsoft.ML; // ML.NET kütüphanesini kullanmak için
using Microsoft.ML.Data; // ML.NET veri sınıflarını kullanmak için
using Newtonsoft.Json; // JSON verilerini işlemek için
using NPOI.XSSF.UserModel; // Excel dosyalarını okumak için
using System; // Genel C# kütüphaneleri
using System.Collections.Generic; // List, KeyValuePair gibi koleksiyonlar için
using System.IO; // Dosya işlemleri için
using System.Net.Http; // HTTP istekleri için
using System.Threading.Tasks; // Asenkron işlemler için
using System.Windows.Forms; // Windows Forms kütüphanesi
using System.Windows.Forms.DataVisualization.Charting; // Grafikler için

namespace FuelPricePrediction.Forms // FuelPricePrediction.Forms namespace'i içindeki frmAnaSayfa sınıfı
{
    public partial class frmAnaSayfa : Form
    {
        // API token
        private readonly string apiKey = "apikey 2kVOVRpkQbvdw8uCLM6sde:07oIihhg5hKgL1nysK3zVv"; // API anahtarını buraya yapıştırın

        private MLContext mlContext; // ML.NET bağlamı
        private ITransformer model; // Eğitilmiş model

        public frmAnaSayfa()
        {
            InitializeComponent(); // Formu başlat
            mlContext = new MLContext(); // ML.NET bağlamını başlat
            TrainModel();
            InitializeChart(); // Chart kontrolünü başlat
        }

        private void TrainModel() // Modeli eğit
        {
            // Çalışma dizinini ve proje kök dizinini belirle
            var projectRoot = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName; // Proje kök dizini
            var dataPath = Path.Combine(projectRoot,"FuelPricePrediction", "data", "fuelPrice.xlsx"); // Excel dosyasının yolu
            var dataView = LoadFromExcel(dataPath, mlContext); // Excel dosyasını yükle

            // Veriyi eğitim ve test setlerine ayır
            var dataSplit = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.2); // %80 eğitim, %20 test
            var trainingData = dataSplit.TrainSet; // Eğitim seti

            // Pipeline oluştur
            var pipeline = mlContext.Transforms.CopyColumns("Label", nameof(AkaryakitFiyati.Price)) // Label sütununu oluştur
                .Append(mlContext.Transforms.Concatenate("Features", nameof(AkaryakitFiyati.Year), nameof(AkaryakitFiyati.Month), nameof(AkaryakitFiyati.Day))) // Features sütununu oluştur
                .Append(mlContext.Regression.Trainers.FastTree()); // Regresyon modeli

            model = pipeline.Fit(trainingData); // Eğitim setiyle modeli eğit
        }

        private void btnPredict_Click(object sender, EventArgs e) // Tahmin yap
        {
            var userDate = dtpDate.Value; // Kullanıcının seçtiği tarih
            var predictor = mlContext.Model.CreatePredictionEngine<AkaryakitFiyati, TahminSonucu>(model); // Tahmin motorunu oluştur
            var prediction = predictor.Predict(new AkaryakitFiyati { Year = userDate.Year, Month = userDate.Month, Day = userDate.Day }); // Tahmini yap

            lblResult.Text = $"Tahmini Fiyat ({userDate.ToShortDateString()}): {prediction.Tahmin}"; // Tahmini fiyatı göster

            
            ShowYearlyPredictions(userDate.Year, predictor); // Yıllık tahmin verilerini hesapla ve grafikte göster
        }

        private void ShowYearlyPredictions(int year, PredictionEngine<AkaryakitFiyati, TahminSonucu> predictor) // Yıllık tahmin verilerini hesapla ve grafikte göster
        {
            var predictions = new List<KeyValuePair<DateTime, float>>(); // Tahmin verileri

            for (int month = 1; month <= 12; month++) // Her ay için tahmin yap
            {
                for (int day = 1; day <= DateTime.DaysInMonth(year, month); day++) // Her gün için tahmin yap
                {
                    var date = new DateTime(year, month, day); // Tarih
                    var prediction = predictor.Predict(new AkaryakitFiyati { Year = year, Month = month, Day = day }); // Tahmini yap
                    predictions.Add(new KeyValuePair<DateTime, float>(date, prediction.Tahmin)); // Tahmini veriyi ekle
                }
            }

            // Grafiğe verileri ekle
            chartPredictions.Series.Clear(); // Önceki verileri temizle
            var series = new Series("Fiyat Tahminleri") // Seri oluştur
            {
                ChartType = SeriesChartType.Line, // Çizgi grafiği
                XValueType = ChartValueType.Date // X ekseninin türü
            };

            foreach (var prediction in predictions) // Tahmin verilerini ekle
            {
                series.Points.AddXY(prediction.Key, prediction.Value); // Veriyi ekle
            }

            chartPredictions.Series.Add(series); // Seriyi ekle
            chartPredictions.Invalidate(); // Grafiği yenile
        }

        private IDataView LoadFromExcel(string path, MLContext mlContext) // Excel dosyasından veri yükle
        {
            var data = new List<AkaryakitFiyati>(); // Veri listesi

            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) // Excel dosyasını aç
            {
                var workbook = new XSSFWorkbook(fs); // Excel dosyasını oku
                var sheet = workbook.GetSheetAt(0); // İlk çalışma sayfasını seç

                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) // Başlık System.IO.DirectoryNotFoundException: ''C:\Users\furka\source\repos\FuelPricePrediction\FuelPricePrediction\FuelPricePrediction\data\fuelPrice.xlsx' yolunun bir parçası bulunamadı.'satırını atla
                {
                    var row = sheet.GetRow(i); // Satırı al
                    if (row == null) continue; // Satır boşsa atla

                    var cellValue = row.GetCell(1).ToString().Trim(); // Hücre değerini al

                    var date = DateTime.Parse(row.GetCell(0).ToString()); // Tarih
                    var price = double.Parse(cellValue); // Fiyat

                    data.Add(new AkaryakitFiyati { Year = date.Year, Month = date.Month, Day = date.Day, Price = (float)price }); // Veriyi ekle
                }
            }

            return mlContext.Data.LoadFromEnumerable(data); // Veriyi yükle
        }

        private async void btnGetir_Click(object sender, EventArgs e) // API'den veri getir
        {
            try
            {
                string selectedCity = comboBoxCities.SelectedItem as string; // Seçilen il

                if (string.IsNullOrEmpty(selectedCity)) // İl seçilmediyse uyarı ver
                {
                    MessageBox.Show("Lütfen bir il seçin."); // Uyarı ver
                    return; // Çık
                }

                // API URL'i (İlçeyi sabit olarak belirleyelim)
                string apiUrl = $"https://api.collectapi.com/gasPrice/turkeyDiesel?district=kadikoy&city={selectedCity.ToLower()}"; 

                // HttpClient oluşturma
                using (HttpClient client = new HttpClient())
                {
                    // HttpClient'a isteği gönderirken authorization başlığını ekleyin
                    client.DefaultRequestHeaders.Add("authorization", apiKey);

                    // HTTP GET isteği gönderme
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    // Yanıtın başarılı olup olmadığını kontrol etme
                    if (response.IsSuccessStatusCode)
                    {
                        // Yanıtın içeriğini okuma
                        string responseData = await response.Content.ReadAsStringAsync();

                        // JSON verisini işle
                        ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseData);

                        if (apiResponse.Success)
                        {
                            // DataGridView'e veriyi yükle
                            dataGridView1.DataSource = apiResponse.Result;
                        }
                        else
                        {
                            MessageBox.Show("API'den başarısız bir yanıt alındı."); // Hata mesajı göster
                        }
                    }
                    else
                    {
                        MessageBox.Show("API'den veri alınamadı. Hata kodu: " + response.StatusCode); // Hata mesajı göster
                    }
                }
            }
            catch (Exception ex) // Hata olursa
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message); // Hata mesajı göster
            }
        }

        private void InitializeChart() // Chart kontrolünü başlat
        {
            // Chart kontrolünü oluştur ve form'a ekle
            chartPredictions = new Chart
            {
                Dock = DockStyle.Bottom, // Alt kısma yasla
                Height = 300 // Yükseklik
            };

            var chartArea = new ChartArea(); // Chart alanı
            chartPredictions.ChartAreas.Add(chartArea); // Chart alanını ekle

            this.Controls.Add(chartPredictions); // Chart kontrolünü ekle
        }

        private Chart chartPredictions; // Tahmin grafiği

        public class AkaryakitFiyati // Akaryakıt fiyatı sınıfı
        {
            public float Year { get; set; }
            public float Month { get; set; }
            public float Day { get; set; }
            public float Price { get; set; }
        }

        public class TahminSonucu // Tahmin sonucu sınıfı
        {
            [ColumnName("Score")]
            public float Tahmin { get; set; }
        }

        public class GasStation // Benzin istasyonu sınıfı
        {
            public string Katkili { get; set; } 
            public string Motorin { get; set; } 
            public string Marka { get; set; } 
        }

        public class ApiResponse // API yanıtı sınıfı
        {
            public bool Success { get; set; }
            public List<GasStation> Result { get; set; }
        }
    }
}
