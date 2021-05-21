IN = $(shell find src/ -name '*.cs')
OUT = project.out
$(OUT): $(IN)
	mcs -pkg:dotnet -lib:/usr/lib/mono/4.5 -unsafe $(IN) -out:$(OUT)

.PHONY: clean
clean:
	rm $(OUT)
