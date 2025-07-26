| Concept            | How it's Used                                           |
| ------------------ | ------------------------------------------------------- |
| **Delegate**       | `GameEventHandler` lets us attach methods dynamically   |
| **Aggregation**    | `Game` class holds a list of event handlers (listeners) |
| **Loose Coupling** | `Game` doesn't know or care *what* handlers do          |
| **Multicast**      | Multiple methods run with `+=` on the delegate          |
