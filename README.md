# MSSQLtoMySQL

This program is a database migration tool, it converts MSSQL databases to MySQL.

## Notice 

This program is unfinished and incomplete. It currently migrate table schema, view schema, and (most of) table data with keys and indexes.
It must be improved in the following topics:
  * Support for all diferent types of indexes and keys
  * Better data type conversion
  * Support blank spaces in table/view name
  * Support database collation
  * Support more MSSQL objects (stored procedures, etc)
  * Code optimization and readability

## Getting Started with Source Code

  * Visual Studio 2015
  * [AeroWizard](https://github.com/dahall/AeroWizard) - Wizard NuGet Package by David Hall
  * [MySQL Connector](https://dev.mysql.com/downloads/windows/visualstudio/) - MySQL Connector Library for Visual Studio
  * Optional: Microsoft SQL Server, MySQL, phpMyAdmin, and a MSSQL database for testing
  
## Contributing

  1. Fork it (https://github.com/yourname/yourproject/fork)
  2. Create your feature branch (git checkout -b feature/fooBar)
  3. Commit your changes (git commit -am 'Add some fooBar')
  4. Push to the branch (git push origin feature/fooBar)
  5. Create a new Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

  * David Hall Wizard Control [AeroWizard](https://github.com/dahall/AeroWizard)
  * Icons:
      * [SPETS.ME](https://spets.me/)
      * [Paomedia](https://www.iconfinder.com/Paomedia)
      * [FatCow Web Hosting](http://www.fatcow.com)
      * [BomSymbols .](https://creativemarket.com/BomSymbols)
      * [Various](http://tango.freedesktop.org/The_People)
