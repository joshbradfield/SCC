SCC
===

Secure Cloud Chat, HTML5 based mutli client secure chat program.

## Direction

HTML5 based web chat written in OPA.

The idea is to have a single executable that can be deployed an run at short notice to create a well functioning chat server which provides secure connections to multiple parties.

Should support:
 - mutliple peer to peer "chats". (These are private, only visible to participants)
 - Sending Files (encrypted)
 - Video and Audio Streaming (also encypted)


## Encyption

The idea is to use public/private key encryption. (separate for each party.)
Each party encrypts their data on their end so that no unsecured data passes through the server.


Public keys are requested by the server for each peer to peer group. So if a chat contains parties A, B and C, A needs to encypt any information with B and C's public key)
I am worried that this may make sycronization hard. Also difficult to "prove" that all parties are recieving the same information.(Perhaps, parties can hash the data and send the hash to the other recipients to confirm that the data sent was identical)
