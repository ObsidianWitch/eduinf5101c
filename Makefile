MCS = gmcs

MCSFLAGS = -pkg:dotnet -lib:/usr/lib/mono/4.5 -unsafe

IN  = src/*.cs src/Views/*.cs src/Math/*.cs src/Models/*.cs src/Lights/*.cs \
      src/Renderers/*.cs
OUT = build/project.out

.PHONY: build clean

build:
	mkdir -p build
	$(MCS) $(MCSFLAGS) $(IN) -out:$(OUT)

clean:
	@rm build/*.out 2>/dev/null || true
