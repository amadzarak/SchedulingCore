# SchedulingCore
SharedAssembly for scheduling related operations.
Methods of scheduling:
- Time Slots (fixed-length slots)
- Time Grain (assign to starting time with granularity)
- Chained through Time (sequence determines times)
- Time Bucket (assign to period without specific ordering)

## Difference between `ScheduleService` and `ScheduleExtensions`
In this library Service Interfaces are defined as operations needing dependencies. For example a EntityFramework for actual database operations.
The ScheduleExtensions allow you to simple perform basic operations on the instantiated object.
