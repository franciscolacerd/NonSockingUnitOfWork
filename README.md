# Non Sucking Unit Of Work

Tired of the same old c# unit of work? Here is a so generic unit of work that I doubt that it works (joking!).

-----


**How to use?**

Copy the code and experiment.

**Create database**

Database is pointing to LocalDB.

Runs the scripts for database generation with migrations:

`add-migration dbdeploy -context NSUOW.Persistence.NsuowDbContext -verbose`

And then:

`update-database -context NSUOW.Persistence.NsuowDbContext -verbose`

**Documentation**

Documentation in the near future.

>Tests in the near future.
