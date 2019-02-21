# poc-expected-deliveries

An event-driven PoC for Expected Deliveries

## Decisions

1. There is value in exploring the event-driven approach.
2. We're going to use document-sourced state. Event-sourcing is YAGNI.

## The Zone of Discomfort

1. Why bother? Is this not just YAGNI? Can we not do a DDD approach using a batch operation?
2. Mutation of state.
3. How do we guarantee the order of events?
4. Static vs singleton vs ctor-injected event sink.
5. When do we create stories/how do we decompose this?
6. Persistence. Where? How?
