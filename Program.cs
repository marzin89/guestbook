using System;
// Importerar Console-klassen
using static System.Console;
// Importerar IO-klassen för filhantering
using System.IO;
// Importerar Collections-klassen för att skapa listor
using System.Collections.Generic;
// Importerar Text.Json för serialisering/deserialisering
using System.Text.Json;

namespace guestbook
{
    public class Entry // Klass som lagrar/returnerar inlägg
    {
        // Fält
        // Namn, text och datum
        private string name;
        private string text;
        private DateTime date;

        // Properties
        // Setters och getters för namn, text och datum
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return this.date;
            }
            set
            {
                this.date = value;
            }
        }
    }

    public class Guestbook // Klass som hämtar, lägger till och raderar inlägg
    {
        // Fält
        // Filnamn
        string fileName = "Guestbook.json";

        // Lista på alla aktuella inlägg
        private List<Entry> entries = new List<Entry>();

        // Konstruerare
        public Guestbook()
        {
            // Om filen finns
            if (File.Exists(fileName))
            {
                // Läser in, deserialiserar och lägger till i listan
                string jsonString = File.ReadAllText(fileName);
                entries = JsonSerializer.Deserialize<List<Entry>>(jsonString);
            }
        }

        // Konstruerare
        public List<Entry> GetAllEntries()
        {
            // Returnerar listan på aktuella inlägg
            return entries;
        }

        // Lägger till ett inlägg
        public Entry AddEntry(Entry entry)
        {
            // Lägger till inlägget i listan, serialiserar och skriver till filen
            entries.Add(entry);
            string jsonString = JsonSerializer.Serialize(entries);
            File.WriteAllText(fileName, jsonString);
            // Returnerar inlägget
            return entry;
        }

        // Raderar ett inlägg
        public int DeleteEntry(int index)
        {
            // Tar bort inlägget ur listan, serialiserar den och skriver till filen
            entries.RemoveAt(index);
            string jsonString = JsonSerializer.Serialize(entries);
            File.WriteAllText(fileName, jsonString);
            // Returnerar index
            return index;
        }

        // Redigerar ett inlägg
        public Entry UpdateEntry(int index, string name, string text)
        {
            // Uppdaterar inlägget i listan, serialiserar och skriver till filen
            entries[index].Name = name;
            entries[index].Text = text;
            entries[index].Date = DateTime.Now.Date;
            string jsonString = JsonSerializer.Serialize(entries);
            File.WriteAllText(fileName, jsonString);
            // Returnerar det redigerade inlägget
            return entries[index];
        }

        // Skriver ut ett inlägg. Metoden används vid uppdatering
        public void WriteEntry(int index)
        {
            // Skriver ut inlägget
            WriteLine($"[{index}] {entries[index].Text}");
            WriteLine($"Av: {entries[index].Name}");
            WriteLine($"Datum: {entries[index].Date}\n");
        }

        // Skriver ut alla inlägg
        public void WriteAllEntries()
        {
            int count = 1;
            // Loopar igenom alla inlägg och skriver ut dem
            foreach(Entry entry in entries)
            {
                WriteLine($"[{count}] {entry.Text}");
                WriteLine($"Av: {entry.Name}");
                WriteLine($"Datum: {entry.Date}\n");
                count++;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Ny instans av gästboken
            Guestbook guestbook = new Guestbook();

            // Säkerställer att programmet startas om efter varje genomfört val (ej X)
            bool run = true;

            while (run == true)
            {
                // Rensar konsolen
                Clear();

                // Kontrollerar om filen finns
                if (File.Exists("Guestbook.json"))
                {
                    // Kontrollerar om det finns inlägg
                    if (guestbook.GetAllEntries().Count > 0)
                    {
                        // Meny om det finns inlägg
                        WriteLine("M A R C O S  G Ä S T B O K\n");
                        WriteLine("Så kul att du hittat hit! Vad vill du göra idag?\n");
                        WriteLine("1. Skriv ett inlägg");
                        WriteLine("2. Ta bort ett inlägg");
                        WriteLine("3. Redigera ett inlägg");
                        WriteLine("X Avsluta\n");
                        // Skriver ut alla aktuella inlägg
                        guestbook.WriteAllEntries();
                    }
                    // Meny om gästboken är tom
                    else
                    {
                        WriteLine("M A R C O S  G Ä S T B O K\n");
                        WriteLine("Så kul att du hittat hit! Vad vill du göra idag?\n");
                        WriteLine("1. Skriv ett inlägg");
                        WriteLine("X Avsluta\n");
                        WriteLine("Gästboken är tom.");
                    }
                }
                // Meny om filen saknas
                else
                {
                    WriteLine("M A R C O S  G Ä S T B O K\n");
                    WriteLine("Så kul att du hittat hit! Vad vill du göra idag?\n");
                    WriteLine("1. Skriv ett inlägg");
                    WriteLine("X Avsluta\n");
                    WriteLine("Gästboken är tom.");
                }

                // Läser in användarens val
                int userChoice = (int) ReadKey(true).Key;

                // Switch-sats baserat på användarens val
                switch (userChoice)
                {
                    // Skriv ett inlägg
                    case 49:
                    {
                        // Ny instans av Entry-klassen
                        var entry = new Entry();

                        // Ber användaren ange sitt namn och läser in detta
                        WriteLine("Ange ditt namn.");
                        string name;

                        // Skriver ut ett felmeddelande så användaren inte har skrivit något
                        while (string.IsNullOrEmpty(name = ReadLine()))
                        {
                            WriteLine("Du måste ange ditt namn.");
                        }

                        // Ber användaren skriva ett inlägg och läser in detta
                        WriteLine("Skriv ett inlägg.");
                        string text;

                        // Skriver ut ett felmeddelande så användaren inte har skrivit något
                        while (string.IsNullOrEmpty(text = ReadLine()))
                        {
                            WriteLine("Du måste skriva ett inlägg.");
                        }

                        // Lagrar alla värden i klassen, serialiserar och skriver till filen
                        entry.Name = name;
                        entry.Text = text;
                        entry.Date = DateTime.Now.Date;
                        guestbook.AddEntry(entry);

                    break;
                    }
                    // Ta bort ett inlägg
                    case 50:
                    {
                        // Ny instans av Entry-klassen
                        var entry = new Entry();

                        // Ber användaren välja ett inlägg att radera och läser in valet
                        WriteLine("Välj ett inlägg ur gästboken (ange nummer).");
                        string index;

                        // Skriver ut ett felmeddelande så användaren inte har skrivit något
                        while (string.IsNullOrEmpty(index = ReadLine()))
                        {
                            WriteLine("Du måste välja ett inlägg.");
                        }

                        // Tar bort inlägget
                        guestbook.DeleteEntry(Convert.ToInt32(index) - 1);
                        
                    break;
                    }
                    // Redigera ett inlägg
                    case 51:
                    {
                        // Ny instans av Entry-klassen
                        var entry = new Entry();

                        // Ber användaren välja ett inlägg att radera och läser in valet
                        WriteLine("Välj ett inlägg ur gästboken (ange nummer).");
                        string index;

                        // Skriver ut ett felmeddelande så användaren inte har skrivit något
                        while (string.IsNullOrEmpty(index = ReadLine()))
                        {
                            WriteLine("Du måste välja ett inlägg.");
                        }

                        // Skriver ut det aktuella inlägget för att underlätta redigeringen
                        guestbook.WriteEntry(Convert.ToInt32(index) - 1);

                        // Ber användaren ange/redigera namnet och läser in detta
                        WriteLine("Redigera namnet.");
                        string name;

                        // Skriver ut ett felmeddelande så användaren inte har skrivit något
                        while (string.IsNullOrEmpty(name = ReadLine()))
                        {
                            WriteLine("Du måste ange ditt namn.");
                        }

                        // Ber användaren skriva/redigera inlägget och läser in detta
                        WriteLine("Redigera inlägget.");
                        string text;

                        // Skriver ut ett felmeddelande så användaren inte har skrivit något
                        while (string.IsNullOrEmpty(text = ReadLine()))
                        {
                            WriteLine("Du måste skriva ett inlägg.");
                        }

                        // Uppdaterar inlägget/listan, serialiserar och skriver till filen
                        guestbook.UpdateEntry(Convert.ToInt32(index) - 1, name, text);
                        
                    break;
                    }
                    // Avsluta
                    case 88:
                    {
                        // Avslutar programmet
                        Environment.Exit(0);
                        break;
                    }
                }
            }
        }
    }
}
