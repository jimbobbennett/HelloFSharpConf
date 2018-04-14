namespace HelloFSharpConf

open Xamarin.Forms
open Xamarin.Forms.Xaml

type MyViewModel () =
    let ev = new Event<_,_>()
    let mutable text = ""

    let createCommand action =
                let event1 = Event<_, _>()
                {
                    new System.Windows.Input.ICommand with
                        member this.CanExecute(obj) = true
                        member this.Execute(obj) = action(obj)
                        member this.add_CanExecuteChanged(handler) = event1.Publish.AddHandler(handler)
                        member this.remove_CanExecuteChanged(handler) = event1.Publish.AddHandler(handler)
                }

    interface System.ComponentModel.INotifyPropertyChanged with 
        [<CLIEvent>]
        member this.PropertyChanged = ev.Publish

    member this.Text
        with get() = text
        and set(value) = text <- value
                         ev.Trigger(this, new System.ComponentModel.PropertyChangedEventArgs("Text"))
    member this.TapCommand = createCommand (fun p -> this.Text <- "Hello F#Conf!")
                 

type HelloFSharpConfPage() =
    inherit ContentPage()
    let _ = base.LoadFromXaml(typeof<HelloFSharpConfPage>)

    do
        base.BindingContext <- new MyViewModel()
        ()