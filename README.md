SCC
===

*Secure Cloud Chat*

- Mutli node chat system.
- Distrubuted (think cluster) file system that is used to drive a chat engine.
- Data is saved independed of interpretation interpreted.
    - To create the links nescissary for a chat system data is created in *data* tables and then *linked* by linking tables.
    - Interpretation and creation of data and links are driven by the view

## Data Management

- Sharded / Distributed data management 
    - Nodes **DO NOT need** to have a copy of all of the data
    - Where data is stored, this data **MUST** be consistant across nodes
    - Nodes **DO NOT need** to know how all tables are formated
    - All data **MUST** be *immutable*
        - Where data is requied to be *mutable*, a table of changes can be built. Servers can choose to keep only the most recent entries in a table.
    - Data Management differs by table.
    - Some Data my not be stored completyl at all
        - Video or Audio in calls could be stored just as handles
- Support new data
    - Nodes can store and distrbute data EVEN IF they dont know about how it is formatted.

The data *is* table based. 

Every table has a 128 bit Guid v1 (timebased) primary key colmumn.

**Table Structure**

| Colmun | Name             | Format              |
| :----: | ---------------- | ------------------- |
| 1      | 128bit Guid      | Guid v1 (timebased) |
| 2..n   | *Table Specific* | *Table Specific*    |   


All IDs for all tables are kept in a master index:

**Master Index Structure**

| Colmun | Name            | Format              |
| :----: | --------------- | ------------------- |
| 1      | Guid            | Guid v1 (timebased) |
| 2      | Table           | Guid v4 (random)    |


All tables, except for the master index are identified with a 128 bit Guid v4 (Random)


## Communication

- The system will be designed to support multiple communication protocols
    - The base protocol will allow change of protocol after a handshake
 
## Encyption / Security

Data can be encrypted if needed

- With mutliple linked tables; each message can be encrypted using the sender and recipient(s) public key. ( Each encrypted copy is stored separately)
    - I am worried that this may make sycronization hard. Also difficult to "prove" that all parties are recieving the same information.(Perhaps, parties can hash the data and send the hash to the other recipients to confirm that the data sent was identical)
    - Public keys can be stored for each each peer to peer group. So if a chat contains parties A, B and C, A needs to encypt any information with B and C's public key
    
- Nodes can communicate with SSL

- Users can manually accept certifcates of other nodes before accepting them into the cluster



