module CartDomain

open NUnit.Framework
open FsUnit

type CartItem = { Name:string; Price: decimal }   

type EmptyState = NoItems // don't use empty list! We want to
                          // force clients to handle this as a 
                          // separate case. E.g. "you have no 
                          // items in your cart"

type ActiveState = { UnpaidItems : CartItem list; }
type PaidForState = { PaidItems : CartItem list; 
                      Payment : decimal}

type Cart = 
    | Empty of EmptyState 
    | Active of ActiveState 
    | PaidFor of PaidForState 

let addToEmptyState item = 
   // returns a new Active Cart
   Cart.Active {UnpaidItems=[item]}

// =============================
// operations on active state
// =============================

let addToActiveState state itemToAdd = 
   let newList = itemToAdd :: state.UnpaidItems
   Cart.Active {state with UnpaidItems=newList }

let removeFromActiveState state itemToRemove = 
   let newList = state.UnpaidItems 
                 |> List.filter (fun i -> i<>itemToRemove)
                
   match newList with
   | [] -> Cart.Empty NoItems
   | _ -> Cart.Active {state with UnpaidItems=newList} 

let payForActiveState state amount = 
   // returns a new PaidFor Cart
   Cart.PaidFor {PaidItems=state.UnpaidItems; Payment=amount}

type EmptyState with
   member this.Add = addToEmptyState 

type ActiveState with
   member this.Add = addToActiveState this 
   member this.Remove = removeFromActiveState this 
   member this.Pay = payForActiveState this 

let addItemToCart cart item =  
   match cart with
   | Empty state -> state.Add item
   | Active state -> state.Add item
   | PaidFor state ->  
       printfn "ERROR: The cart is paid for"
       cart   

let removeItemFromCart cart item =  
   match cart with
   | Empty state -> 
      printfn "ERROR: The cart is empty"
      cart   // return the cart 
   | Active state -> 
      state.Remove item
   | PaidFor state ->  
      printfn "ERROR: The cart is paid for"
      cart   // return the cart


let payForCart cart amount =  
   match cart with
   | Empty state -> 
      printfn "ERROR: The cart is empty"
      cart   // return the cart 
   | Active state -> 
      state.Pay 100m 
   | PaidFor state ->  
      printfn "ERROR: The cart is paid for"
      cart   // return the cart

let displayCart cart  =  
   match cart with
   | Empty state -> 
      printfn "The cart is empty"   // can't do state.Items
   | Active state -> 
      printfn "The cart contains %A unpaid items"
                                                state.UnpaidItems
   | PaidFor state ->  
      printfn "The cart contains %A paid items. Amount paid: %f"
                                    state.PaidItems state.Payment

type Cart with
   static member NewCart = Cart.Empty NoItems
   member this.Add = addItemToCart this 
   member this.Remove = removeItemFromCart this 
   member this.Display = displayCart this 
   member this.Pay = payForCart this 

[<Test>]
let go() =
    let c = Cart.NewCart
    
    printf ""; c.Display


    let i1 = {Name="Paolo"; Price=2323423.00m};

    let c2 = c.Add i1

    printf ""; c2.Display

    let cp = c2.Pay 100m

    printf "paid: "; cp.Display

    let c3 = c2.Remove i1

    printf ""; c3.Display

    true |> should equal true