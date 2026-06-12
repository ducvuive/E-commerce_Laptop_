---
name: goal-griller
description: Grill a user's plan, request, or implementation goals until the goals are focused, verifiable, context-aware, bounded, validated, checkpointed, and governed by stop rules. Use when the user wants to refine goals, stress-test a plan before execution, convert vague intent into actionable success criteria, or mentions goal-griller, goal grilling, good goals, or grill my goals.
---

Turn vague intent into good goals while preserving the relentless interview style of `grill-me`.

Ask one question at a time. For each question, include your recommended answer and why it matters. If the answer can be found by inspecting the codebase, docs, logs, tests, or existing artifacts, inspect them instead of asking the user.

## Sub-Agent Authorization Protocol

This skill is designed to end with an independent completion agent, when available and policy-permitted, that tests whether the finished work satisfies every goal requirement.

- If the user's request explicitly asks for a sub-agent, independent agent, delegated agent, parallel agent, or "agent doc lap" validation, treat that as authorization to spawn the independent completion agent at the final validation gate when multi-agent tooling is available.
- If the user invokes `goal-griller` but does not explicitly authorize sub-agents, ask one short confirmation at the start: "If available and permitted, goal-griller can use a final independent sub-agent audit when the work is done. Do you want me to use that sub-agent?"
- If the user says yes, record that authorization in the working goal spec under `Validation`.
- Before promising a sub-agent audit, confirm the current runtime exposes policy-permitted multi-agent/sub-agent tooling. Use only the actual available multi-agent tool path; do not invent a simulated agent, external process, or unauthorized workaround.
- If the user says no, the tool is unavailable, or runtime/tool policy still blocks spawning, use the fallback self-audit and clearly state why no separate agent was spawned.
- Do not wait until the end to discover missing authorization. Resolve sub-agent permission before execution begins whenever possible.

## Goal Grilling Workflow

Drive every goal through these seven gates before execution or sign-off:

1. **One Outcome**
   - Force the goal to describe one focused outcome.
   - Keep scope larger than a single prompt but smaller than a backlog.
   - Split bundled goals into separate goals when they have different success criteria.

2. **Verifiable End State**
   - Define the binary completion condition: exactly what must be true before stopping.
   - Replace fuzzy words like "better", "robust", "clean", or "done" with observable evidence.
   - Capture the expected artifact, behavior, report, test result, or user-visible state.

3. **Relevant Context**
   - Gather only context needed to execute or validate the goal.
   - Prefer direct artifacts over memory: files and docs, issue links and logs, specs and commands.
   - If context is missing, ask for the smallest missing fact that changes the decision.

4. **Constraints and Non-Goals**
   - State explicit boundaries, especially what must not change.
   - Preserve unrelated behavior, files, generated history, secrets, and user changes.
   - Identify tradeoffs, dependencies, assumptions, and out-of-scope requests.

5. **Validation Loop**
   - Define automated and manual checks before work begins.
   - Choose checks appropriate to the goal: unit/integration tests, lint, typecheck, benchmarks, Playwright/browser verification, artifact inspection, or report consistency checks.
   - Do not treat unverified implementation as complete.

6. **Checkpoint Behavior**
   - Work milestone by milestone.
   - Validate after each checkpoint and repair failures before moving on.
   - Maintain a short progress log with current goal, completed checkpoints, evidence, and open issues.

7. **Stop Rules**
   - Stop when the objective is met and validated.
   - Pause when blocked, stuck, or missing a decision that changes scope.
   - Do not silently expand scope; propose a separate follow-up goal instead.

## Output Contract

When the goal is finalized, produce a compact goal spec:

```markdown
## Goal
[One outcome]

## Completion Criteria
- [Binary, verifiable condition]

## Context
- [Files/docs/logs/specs/commands needed]

## Constraints / Non-Goals
- [Boundaries and what not to change]

## Validation
- [Automated checks]
- [Manual/artifact checks]
- [Independent completion agent authorization: yes/no/runtime-blocked]

## Checkpoints
- [Milestone 1]
- [Milestone 2]

## Stop Rules
- [When to stop]
- [When to pause]
```

## Final Validation Gate

After the agreed goals have been executed, run a final validation gate before giving the final answer. This gate checks the actual result, not just the plan.

1. Reconstruct the full requirement set:
   - original user request;
   - finalized goal spec;
   - all completion criteria;
   - constraints and non-goals;
   - validation checks that were promised;
   - artifacts, file paths, command outputs, screenshots, reports, or logs produced during execution.
2. Verify every promised validation check was actually run or explicitly explain why it could not be run.
3. Do not mark a goal complete from intention alone. Require evidence from artifacts, test output, manual inspection, or command results.
4. If any requirement is incomplete, either repair it before final response or state exactly what remains incomplete and why.

## Independent Completion Agent

As the last step of the final validation gate, create an independent completion review:

1. If multi-agent/sub-agent tooling is available and sub-agent authorization was granted, spawn one independent review agent that did not participate in the main reasoning or execution.
2. Give the independent agent only:
   - the original user request,
   - the final goal spec,
   - the implementation/execution summary,
   - relevant file paths, diffs, command outputs, screenshots, reports, logs, or artifacts needed for verification.
3. Ask that agent to test all requirements from the goal, including constraints and non-goals, and assign for each requirement:
   - `status`: `complete`, `partial`, or `missing`;
   - `completion_percent`: 0-100;
   - concise evidence;
   - concrete follow-up needed for any non-complete goal.
4. Compute percentages consistently:
   - `complete` = 100;
   - `missing` = 0;
   - `partial` = evidence-based 1-99, using subcriteria average when the requirement has explicit subcriteria;
   - default to equal weight per requirement;
   - use weighted scoring only when the goal spec explicitly marks some requirements as higher priority;
   - overall completion percentage = weighted average of requirement completion percentages;
   - constraints and non-goals count as requirements whenever they are included in the finalized goal spec; violations must reduce completion.
5. If multi-agent tooling is not available, authorization was not granted, or runtime/tool policy blocks spawning, perform the same review as a clearly separated "fallback self-audit" pass by re-reading the original request and checking the result against the final artifacts. State the exact reason no separate agent was spawned, and do not call it an independent agent review.
6. Report the final validation result before the final answer, labeling it as either "independent sub-agent validation" or "fallback self-audit":
   - overall completion percentage;
   - per-requirement completion table;
   - remaining risks or unresolved assumptions;
   - whether the work is ready to ship or needs another iteration.
