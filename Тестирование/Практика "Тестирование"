			[TestCase("text", new[] {"text"})]
			[TestCase("hello world", new[] {"hello", "world"})]
			[TestCase("", new string[0])]
			[TestCase("a  b  c", new[] {"a", "b", "c"})]
			[TestCase("  brat  ", new[] {"brat"})]
			[TestCase(@"""abc""", new[] {"abc"})]
			[TestCase("'abc'", new[] {"abc"})]
			[TestCase(@"""""", new[] {""})]
			[TestCase(@"""a b""", new[] {"a b"})]
			[TestCase(@"""a"" ""b""", new[] {"a", "b"})]
			[TestCase(@"""abc", new[] {"abc"})]
			[TestCase(@"""a 'b' c""", new[] {"a 'b' c"})]
			[TestCase(@"""a \""b""", new[] {@"a ""b"})]
			[TestCase(@"""\\""", new[] {@"\"})]
			[TestCase(@"a""b""", new[] {"a", "b"})]
			[TestCase(@"""a""b", new[] {"a", "b"})]
			[TestCase("'a \"b\" c'", new[] {"a \"b\" c"})]
			[TestCase("'dmitriy\\'s'", new[] {"dmitriy's"})]
			[TestCase(@"""aby dabi ", new[] {"aby dabi "})]

            public static void RunTests(string input, string[] expectedOutput)
            {
            // Тело метода изменять не нужно
            Test(input, expectedOutput);
            }
