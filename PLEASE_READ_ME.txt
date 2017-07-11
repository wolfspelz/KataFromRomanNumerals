GalacticMerchant: A Market Value Calculator for a Galactic Merchant

What it does:

  The progam learns about the market and the numbering system 
  and answers question about market values and numbers

tl;dr:

  - This is a console application
  - Visual Studio 2013 Community Edition
  - You can build and run it. Just press F5.
  - It will play the test case from the problem description.

For the Patient Reader:

- Uses MSTest for unit testing:
  - Run All Tests in Visual Studio.

- Calculator is the expert system:
  - It learns and keeps the knowledge base.
  - Then it answers queries.

- The real logic is in the Statements:
  - Statements know how to parse a line and how to execute it.
  - Statements are bound to the Calculator.
  - They draw knowledge from their Calculator.
  - Statements implement IStatement, but most also derive from StatementBase for basic services.

- Silver, Gold, etc. are "Resources", could be TradeGood
  - I like single capital names for the start, if possible. They grow on their own.

- Credits is a fixed term and the only currency because of KISS:
  - A galactic trader might hear about values in different currencies.

- Everything is case sentitive.

- Question marks can be omitted.

- The two query statements could be based on an intermediate class like QueryStatement:
  - But it's ok for now. Maybe when the complexity grows.

- The RomanNumber should be called NumberSystem, because the roman letters are just aliases for certain values:
  - Left it at RomanNumber for readability.

- The numbering system should be pluggable:
  - The calculator has to know it somehow. Now it's static, tightly coupled to the calculator. One and only. 
  - There should be a INumberSystem argument for the Calculator C-tor, but maybe YAGNI, therefore omitted.
  - Might be fun for different unit systems, especially non base-10 (4 finger "Romans").

- Some parts could be more compact, e.g. with Linq and lambdas:
  - But unrolled they are more readable and safe.

- No exceptions in Statement parsers, because failing is a normal op, not an exception:
  - If it's the wrong statement type, just try the next type

- The RomanNumber.ToDecimal could be object oriented:
  - But it's so compact that I doubt that classes would be more readable.

- Command line parser, config, and logger are not production quality and not unit tested:
  - Would use a libraries in the real world.

Documentation:

  - Usage:
    - GalacticMerchant.exe [-i] [-l <log-levels>] 
    - Arguments: 
      - Console interactive mode:
        - Syntax: -i 
        - Description: Wait for command line input. 'quit' quits.
      - Log level: 
        - Syntax: -l <log-levels>
        - Description: 
            Comma separated string of log level names. 
            If you omit 'User', then the console output disappears
        - Example: -l Verbose,Info,User,Warning,Error
        - Values: Any combination of Verbose,Info,User,Warning,Error
        - Default: User,Warning,Error
        
  - Supported statements:
    - Roman Digit Definition: 
      - Syntax: <alias> is <digit>
      - Example: glob is I
      - Values: 
        - <alias>: any non-WSP string
        - <digit>: can be one of (I,V,X,L,C,D,M)
    - Market Value Information: 
      - Syntax: <alias>+ <res> is <value> Credits
      - Example: glob glob Silver is 34 Credits
      - Values: 
        - <alias>+: aliases previously defined by a Roman Digit Definition (multiple, WSP separated)
        - <res>: any non-WSP string
        - <value>: a decimal number
    - Roman Number Query: 
      - Syntax: how much is <alias>+ ?
      - Example: how much is pish tegj glob glob ?
      - Values: 
        - <alias>+: aliases previously defined by a Roman Digit Definition (multiple, WSP separated)
    - Market Value Query: 
      - Syntax: how many Credits is <alias>+ <res> ?
      - Example: how many Credits is glob prok Silver ?
      - Values: 
        - <alias>+: aliases previously defined by a Roman Digit Definition (multiple, WSP separated)
        - <res>: any resource value defined by a prior Market Value Information
