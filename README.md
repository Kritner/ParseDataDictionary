# ParseDataDictionary

There is currently a mostly manual process for updating our data dictionary using an excel document and a variety of VBA macros.  
Rather than do it this way, and continue not having the extended properties in the DB, trying to throw together this little tool that will parse
the current data dictionary excel file, and generate SQL scripts to "catch up" the database to what the document has.

Going forward, I will be putting together another tool that will parse the extended properties within a specified database, 
and recreate this data dictionary in a similar format.  
