/*
╔════════════════════════════════════════════════════════════════════╗
║                           КРЕСТИКИ-НОЛИКИ                          ║
╚════════════════════════════════════════════════════════════════════╝

Вам с Васей наконец-то надоело тренироваться на маленьких программках и вы взялись за настоящее дело! 
Вы решили написать игру крестики-нолики!

Начать было решено с подпрограммы, определяющей не закончилась ли уже игра, 
а если закончилась, то кто выиграл.

Методу GetGameResult передается поле, представленное массивом 3х3 из enum Markers. 
Вам надо вернуть победителя CrossWin или CircleWin, если таковой имеется или Draw, 
если выигрышной последовательности нет ни у одного, либо есть у обоих.

Постарайтесь придумать красивое, понятное решение.

Подумайте, как разбить задачу на более простые подзадачи. 
Попытайтесь выделить один или два вспомогательных метода.

Если вы в затруднении, воспользуйтесь подсказками (кнопка Get hint)
*/

public static GameResult GetGameResult(Mark[,] field)
{
	bool crossWin = false;
	bool circleWin = false;

	for (int x = 0; x < 3; x++)
		{
			int sum = 0;
			for (int y = 0; y < 3; y++)
				sum += (int)field[x, y];
			if (sum == 3 && AllNotEmpty(field[x, 0], field[x, 1], field[x, 2])) crossWin = true;
			if (sum == 6 && AllNotEmpty(field[x, 0], field[x, 1], field[x, 2])) circleWin = true;
		}

	for (int y = 0; y < 3; y++)
		{
			int sum = 0;
			for (int x = 0; x < 3; x++)
				sum += (int)field[x, y];
			if (sum == 3 && AllNotEmpty(field[0, y], field[1, y], field[2, y])) crossWin = true;
			if (sum == 6 && AllNotEmpty(field[0, y], field[1, y], field[2, y]))	circleWin = true;
		}
 
	int d1 = (int)field[0, 0] + (int)field[1, 1] + (int)field[2, 2];
	int d2 = (int)field[0, 2] + (int)field[1, 1] + (int)field[2, 0];

	if (d1 == 3 && AllNotEmpty(field[0, 0], field[1, 1], field[2, 2])) crossWin = true;
	if (d1 == 6 && AllNotEmpty(field[0, 0], field[1, 1], field[2, 2])) circleWin = true;
	if (d2 == 3 && AllNotEmpty(field[0, 2], field[1, 1], field[2, 0])) crossWin = true;
	if (d2 == 6 && AllNotEmpty(field[0, 2], field[1, 1], field[2, 0])) circleWin = true;
			
	if (crossWin && circleWin) return GameResult.Draw;
	if (crossWin) return GameResult.CrossWin;
	if (circleWin) return GameResult.CircleWin;
	return GameResult.Draw;
}

public static bool AllNotEmpty(Mark a, Mark b, Mark c)
{
	return a == b && b == c && a != Mark.Empty;
}
