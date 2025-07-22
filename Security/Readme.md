# Security

## SQL Injection

Simply don't put raw strings into sql command, make sure they are sanitized, which can be done manually or by using some framework...

It's quite easy to avoid / fix and the consquences are extremely bad, so please don't release app with this defect.

## Cross-Site Scripting

Don't use raw user input in any way (In Html, JS, attributes). Attacker can post e.g. js in comment which is then rendered by all other users.

Quite easy to avoid / fix. Validate, sanitize all inputs.

## Sources

- [Hacksplaining](https://hacksplaining.com/lessons)
