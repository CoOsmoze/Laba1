using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace Notebook
{
  class Program
  {
    static void Main(string[] args)
    {
      Menu menu = new Menu(new Book());
    }
  }
  public enum Action
  {
    Exit = 0,
    AddNote,
    RemoveNote,
    EditNote,
    ShortPrintBook,
    PrintBook,
    
  }
  public class Menu
  {
    public Menu(Book book)
    {
      Console.WriteLine("\tЗаписная книжка");
      while (true)
      {
        Console.WriteLine("\nВыберете действие");
        Console.WriteLine($"{(int)Action.AddNote} - Создать запись");
        Console.WriteLine($"{(int)Action.RemoveNote} - Удалить запись");
        Console.WriteLine($"{(int)Action.EditNote} - Изменить запись");
        Console.WriteLine($"{(int)Action.ShortPrintBook} - Список записей с краткой информацией");
        Console.WriteLine($"{(int)Action.PrintBook} - Подробный список записей");
        Console.WriteLine($"{(int)Action.Exit} - Выход\n");
        Console.Write("\tКоманда: ");
        int act = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("----------------------------------------------------------------------------------------------------");
        int id;
        switch (act)
        {
          case (int)Action.AddNote:
            Console.WriteLine("\tСоздание записи");
            book.AddNote(new Note());
            break;
          case (int)Action.RemoveNote:
            Console.WriteLine("Введите номер записи для удаления");
            id = Convert.ToInt32(Console.ReadLine());
            book.RemoveNote(id);
            break;
          case (int)Action.EditNote:
            Console.WriteLine("Введите номер записи для редактирования");
            id = Convert.ToInt32(Console.ReadLine());
            book.EditNote(id);
            break;
          case (int)Action.ShortPrintBook:
            book.ShortReadBook();
            break;
          case (int)Action.PrintBook:
            book.ReadBook();
            break;
          case (int)Action.Exit:
            Console.WriteLine("Завершение работы");
            return;
          default:
            Console.WriteLine("Нет такой команды! Повторите");
            break;
        }
      }
    }
  }
  public class Book
  {
    private int _id = 0;
    private Dictionary<int, Note> _telephoneBook = new Dictionary<int, Note>();
    public void AddNote(Note newNote)
    {
      _telephoneBook.Add(++_id, newNote);
      Console.WriteLine("\tЗапись добавлена");
      Console.WriteLine("----------------------------------------------------------------------------------------------------");
    }
    public void EditNote(int id)
    {
      Console.WriteLine("Изменение записи\n");
      _telephoneBook[id].Edit();
      Console.WriteLine("----------------------------------------------------------------------------------------------------");
    }
    public void RemoveNote(int id)
    {
      Console.WriteLine("Удаление записи\n");
      if (!ReadNote(id))
        return; 
      Console.WriteLine("Вы точно хотите удалить эту запись? Нажмите 0 для отмены, любой другой ввод приведет к удалению записи");
      Console.Write("\tКоманда: ");
      string chek = Console.ReadLine();
      if (chek == "0")
        return;
      _telephoneBook.Remove(id);
      Console.WriteLine("Запись удалена");
      Console.WriteLine("----------------------------------------------------------------------------------------------------");
    }
    public void ReadBook()
    {
      Console.WriteLine("Подробный список записей\n");
      foreach (var item in _telephoneBook)
      {
        Console.WriteLine("Запись номер " + item.Key);
        Console.WriteLine(item.Value);
      }
      Console.WriteLine("----------------------------------------------------------------------------------------------------");
    }
    public void ShortReadBook()
    {
      Console.WriteLine("Список записей с краткой информацией\n");
      foreach(var item in _telephoneBook)
      {
        Console.WriteLine("Запись номер " + item.Key);
        item.Value.ShortPrint();
      }
      Console.WriteLine("----------------------------------------------------------------------------------------------------");
    }
    public bool ReadNote(int id)
    {
      foreach (var item in _telephoneBook)
      {
        if(id == item.Key)
        {
          Console.WriteLine(item.Value);
          return true;
        }
      }
      Console.WriteLine("Такой записи нет");
      return false;
    }
  }
  public enum NoteFields
  {
    Exit = 0,
    Name,
    Surname,
    Patronymic,
    Country,
    Number,
    Birth,
    Organization,
    Position,
    OtherNote
  }
  public class Note
  {
    public string Surname { get; set; }
    public string Name { get; set; }
    public string Number { get; set; }
    public string Country { get; set; }
    public string Patronymic { get; set; }
    public DateTime Birth { get; set; }
    public string Organization { get; set; }
    public string Position { get; set; }
    public string OtherNote { get; set; }
    private string _errorTitle = "Введено некорректное значение! Повторите";
    public Note()
    {
      Console.WriteLine("Обязательно заполните имя, фамилию, номер телефона и страну");

      this.Name = ValidateString("Имя");
      this.Surname = ValidateString("Фамилия");
      this.Number = ValidateNumber("Номер");
      this.Country = ValidateString("Страна");
      string flag;
      Console.WriteLine("Хотите ввести доп информацию? 1 - да, 0 - нет");
      Console.Write("\tКоманда: ");
      flag = Console.ReadLine();
      if (flag != "1")
        return;
      Console.WriteLine("Либо заполните нужные поля, либо пропустите их (enter)");
      this.Patronymic = ValidOther("Отчество");
      this.Birth = ValidateDate("Дата рождения");
      this.Organization = ValidOther("Организация");
      this.Position = ValidOther("Должность");
      Console.Write("\tПрочие заметки: ");
      this.OtherNote = Console.ReadLine();
    }
    public void Edit()
    {
      while (true)
      {
        Console.WriteLine("Выберете поле, которое хотите изменить");
        Console.WriteLine($"{(int)NoteFields.Name} - Имя");
        Console.WriteLine($"{(int)NoteFields.Surname} - Фамилия");
        Console.WriteLine($"{(int)NoteFields.Patronymic} - Отчество");
        Console.WriteLine($"{(int)NoteFields.Country} - Страна");
        Console.WriteLine($"{(int)NoteFields.Number} - Номер телефона");
        Console.WriteLine($"{(int)NoteFields.Birth} - Дата рождения");
        Console.WriteLine($"{(int)NoteFields.Organization} - Организация");
        Console.WriteLine($"{(int)NoteFields.Position} - Должность");
        Console.WriteLine($"{(int)NoteFields.OtherNote} - Прочие заметки");
        Console.WriteLine($"{(int)NoteFields.Exit} - Выход из редактирования");
        Console.Write("\tКоманда: ");
        int field = Convert.ToInt32(Console.ReadLine());
        switch (field)
        {
          case (int)NoteFields.Exit:
            Console.WriteLine("Завершение редактирования \n");
            return;
          case (int)NoteFields.Name:
            this.Name = ValidateString("Имя");
            break;
          case (int)NoteFields.Surname:
            this.Surname = ValidateString("Фамилия");
            break;
          case (int)NoteFields.Number:
            this.Number = ValidateNumber("Номер телефона");
            break;
          case (int)NoteFields.Country:
            this.Country = ValidateString("Страна");
            break;
          case (int)NoteFields.Patronymic:
            this.Patronymic = ValidOther("Очество");
            break;
          case (int)NoteFields.Birth:
            this.Birth = ValidateDate("Дата рождения");
            break;
          case (int)NoteFields.Organization:
            this.Organization = ValidOther("Организация");
            break;
          case (int)NoteFields.Position:
            this.Position = ValidOther("Должность");
            break;
          case (int)NoteFields.OtherNote:
            Console.Write("\tПрочие заметки: ");
            this.OtherNote = Console.ReadLine();
            break;
          default:
            Console.WriteLine("Нет такого поля! Повторите");
            break;
        }
        Console.WriteLine();
        Console.WriteLine(this);
      }
    }
    private string ReadValue(string title)
    {
      Console.Write($"\t{title}: ");
      return Console.ReadLine();
    }
    private string ValidOther(string title = "")
    {
      string value = ReadValue(title);
      if (value.Length == 0)
        return value;
      while (Regex.Match(value, @"\d+").Length != 0)
      {
        Console.WriteLine(_errorTitle);
        value = ReadValue(title);
      }
      return value;
    }
    private string ValidateString(string title = "")
    {
      string value = ReadValue(title);
      while (value.Length == 0 || Regex.Match(value, @"\d+").Length != 0)
      {
        Console.WriteLine(_errorTitle);
        value = ReadValue(title);
      }
      return value;
    }
    private string ValidateNumber(string title = "")
    {
      string value = ReadValue(title);
      while (Regex.Match(value, @"\d+").Length != 10)
      {
        Console.WriteLine(_errorTitle);
        value = ReadValue(title);
      }
      return value;
    }
    private DateTime ValidateDate(string title = "")
    {
      bool flag;
      DateTime value = new DateTime();
      do
      {
        string temp = ReadValue(title);
        if (temp.Length == 0)
          return new DateTime();
        try
        {
          value = Convert.ToDateTime(temp);
          flag = false;
        }
        catch (Exception)
        {
          Console.WriteLine(_errorTitle);
          flag = true;
        }
      } while (flag);
      return value;
    }
    public override string ToString()
    {
      return $"Фамилия: {Surname}\n" +
        $"Имя: {Name}\n" +
        $"Отчество: {Patronymic}\n" +
        $"Номер телефона: {Number}\n" +
        $"Страна: {Country}\n" +
        $"Дата рождения: {(Birth == DateTime.MinValue ? null : Birth.ToLongDateString())}\n" +
        $"Организация: {Organization}\n" +
        $"Должность: {Position}\n" +
        $"Прочие заметки: {OtherNote}\n";
    }
    public void ShortPrint()
    {
      Console.WriteLine($"Фамилия: {Surname}\nИмя: {Name}\nНомер телефона: {Number}\n");
    }
  }
}
