using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace RemoteMonitoring
{
    public class ArduinoUno
    {
        private PeripheryState m_ArduinoPeripheris;

        private List<PeripheryState> m_PreviusState;

        private List<PeripheryState> m_deserializedObjects;

        public ArduinoUno()
        {
            m_ArduinoPeripheris = new PeripheryState();
            m_ArduinoPeripheris.MicrocontrollerPins = new List<MicrocontrollerPin>();
            m_PreviusState = new List<PeripheryState>();
            m_deserializedObjects = new List<PeripheryState>();
        }

        public void AddDigitalInput(string p_Name)
        {
            DigitalInput input = new DigitalInput(p_Name);
            m_ArduinoPeripheris.MicrocontrollerPins.Add(input);
        }

        public void AddDigitalOutput(string p_Name)
        {
            DigitalOutput output = new DigitalOutput(p_Name);
            m_ArduinoPeripheris.MicrocontrollerPins.Add(output);
        }

        public void AddSensorInput(string p_Name)
        {
            SensorInput input = new SensorInput(p_Name);
            m_ArduinoPeripheris.MicrocontrollerPins.Add(input);
        }

        public string GetReadCommand()
        {
            //RD0;RD0;RS0;RS1;@"
            string command = "";

            foreach (MicrocontrollerPin iPin in m_ArduinoPeripheris.MicrocontrollerPins)
            {
                command += iPin.CreateReadCommand();
            }

            command += "@";

            return command;
        }

        public void SetPinsValue(string arduinoAnswer)
        {
            string[] arduinoAnswerSplit = arduinoAnswer.Split(';');
            int cnt = 0;

            foreach (MicrocontrollerPin iPin in m_ArduinoPeripheris.MicrocontrollerPins)
            {
                iPin.SetValue(arduinoAnswerSplit[cnt]);
                if (cnt < arduinoAnswerSplit.Length - 1)
                {
                    cnt++;
                }
            }

            m_ArduinoPeripheris.TimeStamp = DateTime.Now;
            m_PreviusState.Add((PeripheryState)m_ArduinoPeripheris.Clone());
        }

        public void SaveArduinoData()
        {
            string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=ArduinoMonitoring;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    foreach (var state in m_PreviusState)
                    {
                        foreach (var pin in state.MicrocontrollerPins)
                        {
                            // Ellenőrizzük, hogy a PIN már létezik-e a Pins táblában
                            string checkPinQuery = "SELECT COUNT(*) FROM Pins WHERE PinName = @PinName";
                            using (SqlCommand checkPinCommand = new SqlCommand(checkPinQuery, connection))
                            {
                                checkPinCommand.Parameters.AddWithValue("@PinName", pin.Name);
                                int pinExists = (int)checkPinCommand.ExecuteScalar();

                                // Ha a PIN nem létezik, beszúrjuk a Pins táblába
                                if (pinExists == 0)
                                {
                                    string insertPinQuery = "INSERT INTO Pins (PinName, PinType) VALUES (@PinName, @PinType)";
                                    using (SqlCommand insertPinCommand = new SqlCommand(insertPinQuery, connection))
                                    {
                                        insertPinCommand.Parameters.AddWithValue("@PinName", pin.Name);
                                        insertPinCommand.Parameters.AddWithValue("@PinType", pin.GetType().Name); // Típus (SensorInput, DigitalOutput, stb.)
                                        insertPinCommand.ExecuteNonQuery();
                                    }
                                }
                            }

                            // Most már a Measurements táblába menthetjük az adatokat
                            string insertMeasurementQuery = @"
                        INSERT INTO Measurements (PinId, PinValue, TimeStamp)
                        VALUES ((SELECT PinId FROM Pins WHERE PinName = @PinName), @PinValue, @TimeStamp)";

                            using (SqlCommand insertMeasurementCommand = new SqlCommand(insertMeasurementQuery, connection))
                            {
                                insertMeasurementCommand.Parameters.AddWithValue("@PinName", pin.Name);
                                insertMeasurementCommand.Parameters.AddWithValue("@PinValue", pin.GetValueDouble());
                                insertMeasurementCommand.Parameters.AddWithValue("@TimeStamp", state.TimeStamp);
                                insertMeasurementCommand.ExecuteNonQuery();
                            }
                        }
                    }

                    // Sikeres mentés után ürítjük a m_PreviusState listát
                    m_PreviusState.Clear();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hiba történt az SQL mentés során: {ex.Message}");
                }
            }
        }

        /*
        public void SaveArduinoData()
        {
            string filePath = $"savearduinodata_{m_ArduinoPeripheris.TimeStamp.ToString("yyyyMMdd_HH_mm")}.txt";

            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            string json = JsonConvert.SerializeObject(m_PreviusState, settings);

            File.WriteAllText(filePath, json);
            m_PreviusState.Clear();
        }
        */

        /*
        public void SaveArduinoData()
        {
            // SQL kapcsolati string (állítsd be a saját SQL szerver adatbázisod szerint)
            string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=ArduinoMonitoring;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open(); // Kapcsolat megnyitása

                    // Végigmegyünk a menteni kívánt adatokon
                    foreach (var state in m_PreviusState)
                    {
                        foreach (var pin in state.MicrocontrollerPins)
                        {
                            string query = "INSERT INTO PeripheryData (PinName, PinValue, TimeStamp) VALUES (@PinName, @PinValue, @TimeStamp)";

                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                // Paraméterek beállítása az SQL parancshoz
                                command.Parameters.AddWithValue("@PinName", pin.Name);
                                command.Parameters.AddWithValue("@PinValue", pin.GetValueDouble());
                                command.Parameters.AddWithValue("@TimeStamp", state.TimeStamp);

                                command.ExecuteNonQuery(); // SQL parancs végrehajtása
                            }
                        }
                    }

                    // Sikeres mentés esetén az ideiglenes adatokat töröljük
                    m_PreviusState.Clear();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hiba történt az SQL mentés során: {ex.Message}");
                }
            }
        }
        */

        public List<PeripheryState> LoadBackState()
        {
            List<PeripheryState> loadedStates = new List<PeripheryState>();

            string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=ArduinoMonitoring;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open(); // Kapcsolat megnyitása

                    string query = @"
                SELECT P.PinName, M.PinValue, M.TimeStamp
                FROM Measurements M
                JOIN Pins P ON M.PinId = P.PinId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            // Új PeripheryState objektum létrehozása és feltöltése
                            PeripheryState state = new PeripheryState();
                            state.TimeStamp = DateTime.Parse(reader["TimeStamp"].ToString());

                            string pinName = reader["PinName"].ToString();
                            double pinValue = Convert.ToDouble(reader["PinValue"]);

                            // Minden Pin objektumhoz hozzáadjuk a szenzor vagy digitális adatokat
                            MicrocontrollerPin pin = null;

                            if (pinName.StartsWith("S"))
                            {
                                pin = new SensorInput(pinName);
                            }
                            else if (pinName.StartsWith("O"))
                            {
                                pin = new DigitalOutput(pinName);
                            }
                            else if (pinName.StartsWith("I"))
                            {
                                pin = new DigitalInput(pinName);
                            }

                            if (pin != null)
                            {
                                pin.SetValue(pinValue.ToString());
                                state.MicrocontrollerPins.Add(pin);
                            }

                            loadedStates.Add(state);
                        }

                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hiba történt az SQL adatok betöltése során: {ex.Message}");
                }
            }

            return loadedStates;
        }


        /*
        public List<PeripheryState> LoadBackState(string p_directoryPath)
        {
            try
            {
                if (Directory.Exists(p_directoryPath))
                {
                    string[] filePaths = Directory.EnumerateFiles(p_directoryPath, "*.txt")
                    .Where(filePath => Path.GetFileName(filePath).Contains("savearduino"))
                    .ToArray();

                    foreach (string filePath in filePaths)
                    {
                        try
                        {
                            string json = File.ReadAllText(filePath);

                            var settings = new JsonSerializerSettings
                            {
                                TypeNameHandling = TypeNameHandling.Objects // Csak objektum típusokat fog figyelembe venni
                            };

                            List<PeripheryState>? intermediateList = JsonConvert.DeserializeObject<List<PeripheryState>>(json, settings);

                            if (intermediateList != null)
                            {
                                // köztes lista tömbbé alakítása és hozzáadása a kimenő listához
                                m_deserializedObjects.AddRange(intermediateList.ToArray());
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error while reading or deserializing file {filePath}: {ex.Message}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("The given directory doesn't exist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Load error: {ex.Message}");
            }

            return m_deserializedObjects;
        }
        */

        /*public List<PeripheryState> LoadBackState()
        {
            List<PeripheryState> loadedStates = new List<PeripheryState>();

            // SQL kapcsolati string (állítsd be a saját SQL szerver adatbázisod szerint)
            string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=ArduinoMonitoring;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open(); // Kapcsolat megnyitása

                    string query = "SELECT PinName, PinValue, TimeStamp FROM PeripheryData";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            // Új PeripheryState objektum létrehozása és feltöltése
                            PeripheryState state = new PeripheryState();
                            state.TimeStamp = DateTime.Parse(reader["TimeStamp"].ToString());

                            string pinName = reader["PinName"].ToString();
                            double pinValue = Convert.ToDouble(reader["PinValue"]);

                            // Minden Pin objektumhoz hozzáadjuk a szenzor vagy digitális adatokat
                            MicrocontrollerPin pin = null;

                            if (pinName.StartsWith("S"))
                            {
                                pin = new SensorInput(pinName);
                            }
                            else if (pinName.StartsWith("O"))
                            {
                                pin = new DigitalOutput(pinName);
                            }
                            else if (pinName.StartsWith("I"))
                            {
                                pin = new DigitalInput(pinName);
                            }

                            if (pin != null)
                            {
                                pin.SetValue(pinValue.ToString());
                                state.MicrocontrollerPins.Add(pin);
                            }

                            loadedStates.Add(state);
                        }

                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hiba történt az SQL adatok betöltése során: {ex.Message}");
                }
            }

            return loadedStates; // Az összegyűjtött állapotok listájának visszaadása
        }
        */

        public PeripheryState GetArduinoPeripheryState()
        {
            return m_ArduinoPeripheris;
        }
    }
}