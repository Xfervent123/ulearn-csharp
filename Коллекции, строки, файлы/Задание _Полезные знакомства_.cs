/*
╔════════════════════════════════════════════════════════════════════╗
║                        Полезные знакомства                         ║
╚════════════════════════════════════════════════════════════════════╝

В отпуске Вася не тратил время зря, а заводил новые знакомства. Он знакомился с другими крутыми программистами, отдыхающими с ним в одном отеле, и записывал их email-ы.
В его дневнике получилось много записей вида <name>:<email>.
Чтобы искать записи было быстрее, он решил сделать словарь, в котором по двум первым буквам имени можно найти все записи email адресов из его дневника.
Пример: key: Sа -> value: { sasha1995@sasha.ru, alex99@mail.ru, shurik2020@google.com }
Вася уже написал функцию GetContacts, которая считывает его каракули из блокнота. Помогите ему сделать все остальное!
*/

private static Dictionary<string, List<string>> OptimizeContacts(List<string> contacts)
{
	var dictionary = new Dictionary<string, List<string>>();

	foreach (var contact in contacts)
	{
		var parts = contact.Split(':');
		var name = parts[0];
		var prefix = name.Length >= 2 ? name.Substring(0, 2) : name;

		if (!dictionary.ContainsKey(prefix))
		{
			dictionary[prefix] = new List<string>();
		}

		dictionary[prefix].Add(contact);
	}

    return dictionary;
}
