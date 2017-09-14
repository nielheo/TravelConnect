import { Action, Reducer } from 'redux';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.


export interface FlightState {
  search: any;
  result: any[];
  isReturnFlight: boolean;
  selectedDeparture: any;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.
// Use @typeName and isActionType for type detection that works even after serialization/deserialization.

interface SetSearchAction { type: 'SET_SEARCH' }
interface SetResultAction { type: 'SET_RESULT' }
interface SetIsReturnFlightAction { type: 'SET_IS_RETURN_FLIGHT' }
interface SetSelectedDepartureAction { type: 'SET_SELECTED_DEPARTURE' }

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = SetSearchAction | SetResultAction | SetIsReturnFlightAction | SetSelectedDepartureAction

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
  setSearch: () => <SetSearchAction>{ type: 'SET_SEARCH' },
  setResult: () => <SetResultAction>{ type: 'SET_RESULT' },
  setIsReturnFlight: () => <SetIsReturnFlightAction>{ type: 'SET_IS_RETURN_FLIGHT' },
  setSelectedDeparture: () => <SetSelectedDepartureAction>{ type: 'SET_SELECTED_DEPARTURE' },
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

export const reducer: Reducer<FlightState> = (state: FlightState, action: KnownAction) => {
  switch (action.type) {
    case 'SET_SEARCH':
      return { ...state, search: state.search };
    case 'SET_RESULT':
      return { ...state, result: state.result };
    case 'SET_IS_RETURN_FLIGHT':
      return { ...state, isReturnFlight: state.isReturnFlight };
    case 'SET_SELECTED_DEPARTURE':
      return { ...state, selectedDeparture: state.selectedDeparture };
    default:
      // The following line guarantees that every action in the KnownAction union has been covered by a case above
      const exhaustiveCheck: never = action;
  }

  // For unrecognized actions (or in cases where actions have no effect), must return the existing state
  //  (or default initial state if none was supplied)
  return state || { search: null, result: null, returnFlight: false, selectedDepart: null };
};
